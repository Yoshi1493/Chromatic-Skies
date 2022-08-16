using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const float SpawnTime = 1f;
    const float FollowTime = 5f;
    const float FollowDelay = 0.4f;
    const float SpawnOffsetRadius = 0.5f;

    List<EnemyBullet> bullets = new();
    Queue<Vector3> playerPositions = new();

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float t = 0f;
            float x = (Random.Range(0, screenHalfWidth * 0.25f) + screenHalfWidth * 0.5f) * PositiveOrNegativeOne;
            Vector3 v1 = new(x, screenHalfHeight);            

            while (t < FollowTime)
            {
                float z = RandomAngleDeg;
                Vector3 pos = SpawnOffsetRadius * Random.insideUnitCircle;

                if (t > SpawnTime - FollowDelay)
                {
                    playerPositions.Enqueue(PlayerPosition);
                }

                if (t < SpawnTime)
                {
                    Vector3 v2 = new(x, PlayerPosition.y);
                    Vector3 v3 = QuadraticBezierCurve(v1, v2, PlayerPosition, t / SpawnTime);
                    pos += v3;
                }
                else
                {
                    pos += playerPositions.Dequeue();
                }

                var bullet = SpawnProjectile(0, z, pos, false);
                bullets.Add(bullet);

                yield return WaitForSeconds(ShootingCooldown);
                t += ShootingCooldown;
            }

            yield return WaitForSeconds(1f);

            SetSubsystemEnabled(1);

            bullets.Clear();
            playerPositions.Clear();

            yield return WaitForSeconds(5f);
        }
    }
}