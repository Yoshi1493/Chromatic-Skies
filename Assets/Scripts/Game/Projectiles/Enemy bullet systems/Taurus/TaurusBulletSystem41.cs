using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem41 : EnemyShooter<Laser>
{
    const int LaserCount = 0;

    protected override float ShootingCooldown => base.ShootingCooldown;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < LaserCount; i++)
        {
            float z = 0f;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}