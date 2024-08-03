using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem63 : EnemyShooter<Projectile>
{
    const int BulletCount = 0;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float z = 0;
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(3, z, pos);
        }

        yield return WaitForSeconds(ShootingCooldown);

        enabled = false;
    }
}