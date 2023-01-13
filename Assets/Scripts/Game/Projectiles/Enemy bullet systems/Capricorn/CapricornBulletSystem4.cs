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
            Vector3 v0 = new(x, screenHalfHeight);

            while (t < TotalFollowTime)
            {
                if (t > FollowDelay)
                {
                    playerPositions.Enqueue(PlayerPosition);
                }

                Vector2 pos;

                if (t < CatchupTime)
                {
                    Vector3 v1 = new(x, PlayerPosition.y);
                    Vector3 v2 = QuadraticBezierCurve(v0, v1, PlayerPosition, t / CatchupTime);
                    pos = v2;

                    if (t > SpawnTime)
                    {
                        Vector3 v3 = Vector3.Lerp(v2, playerPositions.Dequeue(), (t - SpawnTime) / (CatchupTime - SpawnTime));
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

            AttackFinishAction?.Invoke();
            yield return WaitForSeconds(6f);
        }
    }
}