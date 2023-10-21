using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = -i * BulletSpacing;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(1, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}