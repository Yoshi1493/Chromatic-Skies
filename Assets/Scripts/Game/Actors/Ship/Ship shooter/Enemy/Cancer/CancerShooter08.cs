using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerShooter08 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 44;
    const float WaveSpacing = 15f;
    const int BulletCount = 2;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        bulletData.colour = bulletData.gradient.Evaluate(Random.value);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ((ii % 2 * 2 - 1) * i * WaveSpacing) + 180f;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(8, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}