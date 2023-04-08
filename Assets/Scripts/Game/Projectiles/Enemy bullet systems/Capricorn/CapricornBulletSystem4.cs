using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const float SpawnTime = 1f;
    const float CatchupTime = 2f;
    const float TotalFollowTime = 5f;
    const float FollowDelay = 0.2f;
    const float SpawnOffsetRadius = 0.5f;

    Queue<Vector3> playerPositions = new();

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float t = 0f;
            float x = (Random.Range(0, screenHalfWidth * 0.25f) + screenHalfWidth * 0.5f) * Mathf.Sign(ownerShip.transform.position.x);

            QuadraticBezierCurve curve = new();
            curve.v0 = new(x, screenHalfHeight);

            while (t < TotalFollowTime)
            {
                if (t > FollowDelay)
                {
                    playerPositions.Enqueue(PlayerPosition);
                }

                Vector2 pos;

                if (t < CatchupTime)
                {
                    curve.v1 = new(x, PlayerPosition.y);
                    curve.v2 = PlayerPosition;
                    pos = curve.Evaluate(t / CatchupTime);

                    if (t > SpawnTime)
                    {
                        Vector3 v3 = Vector3.Lerp(pos, playerPositions.Dequeue(), (t - SpawnTime) / (CatchupTime - SpawnTime));
                        pos = v3;
                    }
                }
                else
                {
                    pos = playerPositions.Dequeue();
                }

                pos += (SpawnOffsetRadius * Random.insideUnitCircle);
                float z = RandomAngleDeg;

                bulletData.colour = bulletData.gradient.Evaluate(t / TotalFollowTime);
                SpawnProjectile(0, z, pos, false).Fire();

                yield return WaitForSeconds(ShootingCooldown);
                t += ShootingCooldown;
            }

            playerPositions.Clear();

            yield return WaitForSeconds(1f);
            SetSubsystemEnabled(1);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(6f);
        }
    }
}