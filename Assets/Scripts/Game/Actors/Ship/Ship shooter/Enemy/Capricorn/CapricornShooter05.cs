using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornShooter05 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 9;
    const float BulletSpacing = 10f;
    const float BulletSpawnRadius = 0.1f;
    const float BulletSpawnRadiusModifier = 0.1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < BulletCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);
            float z = ((i - ((BulletCount - 1) / 2f)) * BulletSpacing) + r;
            Vector3 pos = (BulletSpawnRadius + (i * BulletSpawnRadiusModifier)) * transform.up.RotateVectorBy(z);

            bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));

            SpawnProjectile(5, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}