using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem2 : EnemyShooter<EnemyBullet>
{
    [SerializeField] ProjectileObject[] bulletData = new ProjectileObject[2];

    const int WaveCount = 1;
    const int BulletCount = 30;

    Queue<EnemyBullet> bullets = new Queue<EnemyBullet>(BulletCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        SetSubsystemEnabled(1);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int j = 0; j < BulletCount; j++)
            {
                float bulletSpacing = 360f / BulletCount;
                float z = j * bulletSpacing;

                bulletData[0].colour = bulletData[0].gradient.Evaluate((float)j / BulletCount);
                bullets.Enqueue(SpawnProjectile(0, z, transform.up.RotateVectorBy(z) * 2f));

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 9f);

            for (int j = 0; j < BulletCount; j++)
            {
                float bulletSpacing = 360f / BulletCount;
                float z = j * bulletSpacing;

                var enemyBullet = bullets.Dequeue();
                Vector3 spawnPos = enemyBullet.transform.position;

                bulletData[1].colour = bulletData[1].gradient.Evaluate((float)j / BulletCount);

                SpawnProjectile(1, z, spawnPos, false).Fire();
                SpawnProjectile(1, z + 180f, spawnPos, false).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

        }

        yield return ownerShip.MoveToRandomPosition(1f, delay: 10f);
    }
}