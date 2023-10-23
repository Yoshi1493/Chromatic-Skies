using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BranchCount = 18;
    const float BranchSpacing = 360f / BranchCount;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount);

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        float r = Random.Range(0f, BranchSpacing);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveCount) + (ii * BranchSpacing) + r;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(1, z, pos) as AquariusBullet11;
                bullet.DestroyAction += OnSpawnedBulletDestroy;
                bullet.StartCoroutine(bullet.LerpSpeed(WaveCount - i, 0f, 1f));
                bullets.Add(bullet);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        for (int i = WaveCount - 1; i >= 0; i--)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                int b = (i * BranchCount) + ii;
                bullets[b].Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        bullets.Clear();
        enabled = false;
    }

    void OnSpawnedBulletDestroy(EnemyBullet bullet)
    {
        bullets.Remove(bullet);
    }
}