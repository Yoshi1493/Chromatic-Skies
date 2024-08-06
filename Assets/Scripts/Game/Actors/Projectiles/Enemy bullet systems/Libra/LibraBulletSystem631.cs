using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem631 : EnemyShooter<Laser>
{
    const int LaserCount = 15;
    const float LaserSpawnRadius = 1.5f;

    List<Laser> lasers = new(LibraBulletSystem63.ParentBulletCount);
    List<Laser> childLasers = new(LibraBulletSystem63.ParentBulletCount * LaserCount);

    protected override IEnumerator Shoot()
    {
        lasers.Clear();

        for (int i = 0; i < LibraBulletSystem63.ParentBulletCount; i++)
        {
            float z = i * LibraBulletSystem63.ParentBulletSpacing;
            Vector3 pos = LaserSpawnRadius * transform.up.RotateVectorBy(z + 180f);

            var laser = SpawnProjectile(0, z, pos);
            laser.Fire(1f);
            lasers.Add(laser);
        }

        yield return WaitForSeconds(ShootingCooldown);
        enabled = false;
    }
}