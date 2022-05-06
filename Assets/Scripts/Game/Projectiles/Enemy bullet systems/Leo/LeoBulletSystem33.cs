using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem33 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;
    protected override float ShootingCooldown => 2.3f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                SpawnProjectile(2, z, Vector3.zero).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}