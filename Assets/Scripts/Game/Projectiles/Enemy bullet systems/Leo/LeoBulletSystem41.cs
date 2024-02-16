using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 0;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = 0;
                SpawnProjectile(0, z, Vector3.zero).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}