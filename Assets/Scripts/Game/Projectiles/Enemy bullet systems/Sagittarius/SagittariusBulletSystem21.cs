using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 36;
    const float WaveSpacing = 360f / WaveCount / BranchCount;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = WaveSpacing * 2f;
    const float BulletSpacingSpacing = 30f;
    const float BulletSpawnRadius = 2.5f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount * BulletCount);

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = i * BulletSpacing + (iii * BulletSpacingSpacing);
                        Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy((i * WaveSpacing) + (ii * BranchSpacing));

                        bullets.Add(SpawnProjectile(1, z, pos));
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            for (int i = 0; i < BulletCount; i++)
            {
                for (int ii = 0; ii < WaveCount * BranchCount; ii++)
                {
                    int b = ii * BulletCount + i;
                    StartCoroutine(bullets[b].LerpSpeed(0f, 3f, 1f));
                }

                yield return WaitForSeconds(0.5f);
            }

            bullets.Clear();
            yield return WaitForSeconds(10f);
        }
    }
}