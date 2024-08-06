using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem63 : EnemyShooter<Projectile>
{
    const int ParentBulletCount = 8;
    const float ParentBulletSpacing = 360f / ParentBulletCount;

    protected override IEnumerator Shoot()
    {
        enabled = false;
        yield break;

        for (int i = 0; i < ParentBulletCount; i++)
        {
            float z = i * ParentBulletSpacing;
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(5, z, pos) as EnemyBullet;
            bullet.Fire();
        }

        yield return WaitForSeconds(ShootingCooldown);

        enabled = false;
    }
}