using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class GeminiShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 32;
    const int BulletCount = 2;
    const float BulletSpacing = 0.32f;
    const float BulletSpawnRadius = 2f;
    const float BulletRotationSpeed = 64;
    const float BulletRotationDuration = 1.6f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = RandomAngleDeg;
            float r = Random.Range(0.1f, 1f) * BulletSpawnRadius;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                int d = ii % 2 * 2 - 1;
                Vector3 pos = ((r * transform.up) + (d * BulletSpacing * transform.right)).RotateVectorBy(z);
                bulletData.colour = bulletData.gradient.Evaluate(ii);

                var bullet = SpawnProjectile(3, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationSpeed, BulletRotationDuration, delay: 2.5f));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}