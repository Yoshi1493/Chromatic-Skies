using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BulletSpacing / 2f;
    const int BulletCount = 24;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 2f;
    const float SpawnRadiusMultiplier = 0.5f;

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            Vector3 v1 = PlayerPosition;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BulletSpacing);
                    Vector3 pos = v1 + (BulletSpawnRadius + (i * SpawnRadiusMultiplier)) * Vector3.up.RotateVectorBy(z);

                    SpawnProjectile(0, z, pos, false).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(10f);
        }
    }
}