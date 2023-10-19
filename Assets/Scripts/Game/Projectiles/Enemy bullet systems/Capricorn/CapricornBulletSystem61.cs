using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 12;
    const float BulletSpacing = 180f / BulletCount;

    List<EnemyBullet> bullets = new(BulletCount);

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i - ((BulletCount - 1) / 2f)) * BulletSpacing;
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(1, z, pos);
            bullet.StartCoroutine(bullet.LerpSpeed(5f, 0f, 1f));
            bullets.Add(bullet);
        }

        bullets.Randomize();

        yield return WaitForSeconds(2f);

        for (int i = 0; i < BulletCount; i++)
        {
            bullets[i].Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }

        bullets.Clear();
        enabled = false;
    }
}