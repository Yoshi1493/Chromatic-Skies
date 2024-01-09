using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 24;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 1.6f;

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2f);

            Vector3 v = PlayerPosition;

            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = (BulletSpawnRadius * Vector3.up.RotateVectorBy(z)) + v;

                SpawnProjectile(1, z, pos, false).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);
        }
    }
}