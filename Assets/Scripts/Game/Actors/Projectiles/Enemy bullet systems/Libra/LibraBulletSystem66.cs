using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem66 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 0;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float z = 0;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(10, z, pos).Fire();
        }

        yield return WaitForSeconds(ShootingCooldown);

        enabled = false;
    }
}