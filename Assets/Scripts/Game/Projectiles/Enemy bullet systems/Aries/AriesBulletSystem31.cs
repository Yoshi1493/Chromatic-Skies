using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int RingCount = 3;
    const int BaseBulletCount = 18;
    const int BulletCountModifier = 6;
    const float BulletSpawnRadius = 1.5f;
    const float SpawnRadiusModifier = 0.5f;
    const float BulletRotationSpeed = 30f;
    const float BulletRotationSpeedModifier = 15f;

    List<EnemyBullet> bullets = new();

    protected override float ShootingCooldown => 1 / 60f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        Vector3 v1 = 2f * Vector3.down;

        for (int i = 0; i < RingCount; i++)
        {
            int bulletCount = BaseBulletCount + (i * BulletCountModifier);
            float bulletSpacing = 360f / bulletCount;
            int r = i % 2 * 2 - 1;

            for (int ii = 0; ii < bulletCount; ii++)
            {
                float z = r * ii * bulletSpacing;
                float t = r * 90f;
                Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(z - t) + v1;

                bulletData.colour = bulletData.gradient.Evaluate(i / (RingCount - 1f));

                bullets.Add(SpawnProjectile(1, z, pos, false));
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.1f);
        }

        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < RingCount; i++)
        {
            int bulletCount = BaseBulletCount + (i * BulletCountModifier);

            for (int ii = 0; ii < bulletCount; ii++)
            {
                float s = BulletRotationSpeed + (i * BulletRotationSpeedModifier);
                bullets[0].StartCoroutine(bullets[0].RotateAround(2f * Vector3.down, Mathf.Infinity, s, i % 2 == 0));
                bullets.RemoveAt(0);
            }
        }
    }
}