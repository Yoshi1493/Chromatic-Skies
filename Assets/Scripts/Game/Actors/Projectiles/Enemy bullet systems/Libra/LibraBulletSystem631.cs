using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem631 : EnemyShooter<Laser>
{
    const int LaserCount = 15;
    const float LaserSpawnRadius = 1.5f;
    const float SpawnMaxAngle = 60f;
    const float FireDelay = 2f;

    List<Laser> lasers = new(LibraBulletSystem63.ParentBulletCount);
    List<Laser> childLasers = new(LibraBulletSystem63.ParentBulletCount * LaserCount);

    protected override IEnumerator Shoot()
    {
        lasers.Clear();

        for (int i = 0; i < LibraBulletSystem63.ParentBulletCount; i++)
        {
            float z = (i + 0.5f) * LibraBulletSystem63.ParentBulletSpacing;
            Vector3 pos = LaserSpawnRadius * transform.up.RotateVectorBy(z + 180f);

            var laser = SpawnProjectile(0, z, pos);
            laser.Fire(1f);
            lasers.Add(laser);
        }

        yield return WaitForSeconds(1f);

        float spawnRadius = 0f;

        for (int i = 0; i < LaserCount; i++)
        {
            float r = Random.Range(-SpawnMaxAngle, SpawnMaxAngle);
            float d = FireDelay - (i * ShootingCooldown);

            for (int ii = 0; ii < LibraBulletSystem63.ParentBulletCount; ii++)
            {
                Transform l = lasers[ii].transform;
                float z = l.eulerAngles.z + r;
                Vector3 pos = l.position + (spawnRadius * l.up);

                var laser = SpawnProjectile(1, z, pos, false);
                laser.Fire(d);
            }

            spawnRadius += Random.value;
            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}