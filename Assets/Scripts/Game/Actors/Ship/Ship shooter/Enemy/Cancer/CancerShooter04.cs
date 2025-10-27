using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerShooter04 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 13;
    const float BulletSpacing = 10f;
    const float BulletSpawnRadius = 0.8f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.8f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < BulletCount; i++)
        {
            int d = i % 2;
            float z = ((i - ((BulletCount - 1) / 2f)) * BulletSpacing) + r;
            Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z);

            bulletData.colour = bulletData.gradient.Evaluate(d);

            var bullet = SpawnProjectile(4, z, pos);
            bullet.StartCoroutine(bullet.LerpSpeed(4f, 3f, 0.5f, delay: 0.5f + (i * ShootingCooldown)));

            if (d == 1)
            {
                bullet.StartCoroutine(bullet.HomeInOn(playerShip, 1.2f));
            }

            bullet.Fire();
        }
    }
}