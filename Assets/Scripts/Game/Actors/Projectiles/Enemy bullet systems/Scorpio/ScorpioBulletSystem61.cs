using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int RingCount = 12;
    const int BulletCount = 12;
    const int BulletCountModifier = 6;
    const float BulletSpawnRadius = 0.1f;
    const float SpawnRadiusModifier = 1f;

    List<EnemyBullet> bullets = new(RingCount * BulletCount);

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < RingCount; i++)
        {
            int bulletCount = BulletCount + (i * BulletCountModifier);
            float bulletSpacing = 360f / bulletCount;

            for (int ii = 0; ii < bulletCount; ii++)
            {
                float z = ii * bulletSpacing;
                Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(z);

                bulletData.colour = bulletData.gradient.Evaluate(i / (RingCount - 1f));

                var bullet = SpawnProjectile(3, z, pos);
                bullets.Add(bullet);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(0.5f);

        bullets.ForEach(b => b.Fire());

        enabled = false;
    }
}