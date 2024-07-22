using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class VirgoBulletSystem22 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 50;
    readonly float WaveSpacing = (1f + Mathf.Sqrt(5f)) * 180f;
    const int BulletCount = 5;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.4f;

    protected override IEnumerator Shoot()
    {
        int d = PositiveOrNegativeOne;

        for (int i = 0; i < WaveCount; i++)
        {
            bulletData.colour = bulletData.gradient.Evaluate(Random.value);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = d * (i * WaveSpacing % 360f);
                float t = (ii * BulletSpacing) + z;
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}
