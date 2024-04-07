using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;
using static BezierHelper;

public class CapricornBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const float TotalFollowTime = 5f;
    const float SpawnTime = 0.6f;
    const float CatchupTime = 3f;
    const float FollowDelay = 0.2f;
    const float SpawnOffsetRadius = 0.5f;

    Queue<Vector3> playerPositionData = new();

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float t = 0f;
            float x = screenHalfWidth * Random.Range(0.5f, 0.8f) * Mathf.Sign(ownerShip.transform.position.x);

            Vector3 v0 = new(x, screenHalfHeight);

            while (t < TotalFollowTime)
            {
                if (t > FollowDelay)
                {
                    playerPositionData.Enqueue(PlayerPosition);
                }

                float z = RandomAngleDeg;
                Vector3 pos;

                if (t < CatchupTime)
                {
                    Vector3 v1 = new(x, PlayerPosition.y);
                    Vector3 v2 = PlayerPosition;
                    pos = EvaluateQuadratic(v0, v1, v2, t / CatchupTime);

                    if (t > SpawnTime)
                    {
                        if (playerPositionData.TryDequeue(out Vector3 p))
                        {
                            float f = (t - SpawnTime) / (CatchupTime - SpawnTime);
                            Vector3 v3 = Vector3.Lerp(pos, p, f);
                            pos = v3;
                        }
                    }
                }
                else
                {
                    pos = playerPositionData.Dequeue();
                }

                pos += SpawnOffsetRadius * (Vector3)Random.insideUnitCircle;

                bulletData.colour = bulletData.gradient.Evaluate(t / TotalFollowTime);

                SpawnProjectile(0, z, pos, false).Fire();

                yield return WaitForSeconds(ShootingCooldown);
                t += ShootingCooldown;
            }

            playerPositionData.Clear();
            SetSubsystemEnabled(1);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(6f);
        }
    }
}