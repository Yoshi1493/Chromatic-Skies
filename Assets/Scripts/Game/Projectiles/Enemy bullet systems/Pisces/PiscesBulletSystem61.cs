using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int LaserCount = 0;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < LaserCount; i++)
            {
                float z = 0;
                SpawnProjectile(0, z, Vector3.zero).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}