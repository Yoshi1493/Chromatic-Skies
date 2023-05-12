using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 12;
    const float WaveSpacing = 5f;
    const int BranchCount = 20;
    const float BranchSpacing = 360f / BranchCount;
    const float RingSpacing = 0.3f;

    Stack<EnemyBullet> bullets = new(WaveCount * BranchCount);

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
                    Vector3 pos = i * RingSpacing * transform.up.RotateVectorBy(z);
                    z += 90f;

                    bullets.Push(SpawnProjectile(1, z, pos));
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    bullets.Pop().Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = -(i * WaveSpacing) - (ii * BranchSpacing);
                    Vector3 pos = i * RingSpacing * transform.up.RotateVectorBy(z);
                    z -= 90f;

                    bullets.Push(SpawnProjectile(2, z, pos));
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    bullets.Pop().Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }
        }
    }
}