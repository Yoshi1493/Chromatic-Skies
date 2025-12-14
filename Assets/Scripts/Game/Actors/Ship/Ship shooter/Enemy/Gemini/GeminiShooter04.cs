using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter04 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 4;
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.24f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(0.5f);

        float z = PlayerPosition.GetRotationDifference(transform.position) + Random.Range(-10f, 10f);

        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float t = (ii * BulletSpacing) + z;
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                bulletData.colour = bulletData.gradient.Evaluate(ii % 2);

                SpawnProjectile(4, z, pos).Fire();
            }
        }
    }
}