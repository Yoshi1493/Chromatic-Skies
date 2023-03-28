using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 22;
    const float WaveSpacing = -3f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedMultiplier = 0.1f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = 2f * Vector3.forward;

                    bullets.Add(SpawnProjectile(0, z, pos));
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    int b = (i * BranchCount) + ii;

                    float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                    bullets[b].MoveSpeed = s;
                    bullets[b].Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            bullets.Clear();
        }
    }
}