using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;

    List<EnemyBullet> bullets = new(BulletCount);

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(1, z, pos);
                bullet.Fire();
                bullets.Add(bullet);
            }

            yield return WaitForSeconds(100f);
        }
    }
}