using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter08 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const int BulletCount = 3;
    const float BulletSpacing = 60f;
    const float BulletSpawnOffset = 0.5f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float r = ((ii - ((BulletCount - 1) / 2f)) * BulletSpacing) + z;
                Vector3 pos = BulletSpawnOffset * transform.up.RotateVectorBy(r);

                bulletData.colour = bulletData.gradient.Evaluate(ii % 2);

                SpawnProjectile(8, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}