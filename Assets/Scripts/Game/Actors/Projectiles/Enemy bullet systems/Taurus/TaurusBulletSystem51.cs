using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem51 : EnemyShooter<EnemyBullet>
{
    public const int BulletCount = 10;
    const float BulletSpacing = 360f / BulletCount;
    public const float BulletSpawnRadius = 20f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        while (enabled)
        {
            Vector3 v = PlayerPosition;

            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = (BulletSpawnRadius * transform.up.RotateVectorBy(z)) + v;

                SpawnProjectile(1, z, pos, false).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(12f);
        }
    }
}