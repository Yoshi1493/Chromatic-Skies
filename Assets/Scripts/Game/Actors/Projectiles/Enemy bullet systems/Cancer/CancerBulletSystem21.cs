using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 32;
    const float WaveSpacing = 360f / WaveCount / BranchCount;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 4f;

    List<EnemyBullet> bullets = new(WaveCount);

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.2f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = -(i * WaveSpacing) - (ii * BranchSpacing);
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(1, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed, 0f, 0.5f));
                bullets.Add(bullet);
            }
        }

        bullets.Randomize();
        yield return WaitForSeconds(1.2f);

        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }

        bullets.Clear();
        enabled = false;
    }
}