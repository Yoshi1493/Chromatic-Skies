using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem62 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 20;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));

                SpawnProjectile(2, z, pos).Fire();
            }
        }
    }
}