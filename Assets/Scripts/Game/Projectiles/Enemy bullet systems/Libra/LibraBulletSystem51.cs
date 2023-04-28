using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem51 : EnemyShooter<Laser>
{
    const int LaserCount = 20;
    const float LaserSpacing = 360f / LaserCount;

    protected override float ShootingCooldown => 2f / LaserCount;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < LaserCount; i++)
        {
            float z = i * LaserSpacing;
            Vector3 pos = Vector3.zero;

            bulletData.colour = bulletData.gradient.Evaluate(i / (LaserCount - 1f));
            SpawnProjectile(0, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}