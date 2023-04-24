using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 12;
    const float WaveSpacing = 5f;
    const int BranchCount = 20;
    const float BranchSpacing = 360f / BranchCount;

    Stack<EnemyBullet> bullets = new(WaveCount * BranchCount);

    //protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = i * 0.4f * transform.up.RotateVectorBy(z);
                    z += 90f;

                    bullets.Push(SpawnProjectile(1, z, pos));
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    bullets.Pop().Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}