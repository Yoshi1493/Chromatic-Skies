using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 32;
    const float ArcHalfWidth = 90f;
    const int BulletCount = 2;
    const float BulletSpacing = 0.16f;
    const float BulletSpawnRadius = 2f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position) +  Random.Range(-ArcHalfWidth, ArcHalfWidth);
            float r = Random.Range(0.1f, 1f) * BulletSpawnRadius;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                int d = ii % 2 * 2 - 1;
                Vector3 pos = ((r * transform.up) + (d * BulletSpacing * transform.right)).RotateVectorBy(z);
                bulletData.colour = bulletData.gradient.Evaluate(ii);

                SpawnProjectile(3, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}