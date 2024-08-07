using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LibraBulletSystem631 : EnemyShooter<Laser>
{
    const int ParentBulletCount = LibraBulletSystem63.ParentBulletCount;
    const int LaserCount = 15;
    const float LaserSpawnRadius = 1f;
    const float SpawnMaxAngle = 60f;
    const float FireDelay = 1f;
    const int ChildLaserCount = 2;

    List<Laser> lasers = new(ParentBulletCount);
    List<Laser> childLasers = new(ParentBulletCount * LaserCount);

    protected override IEnumerator Shoot()
    {
        lasers.Clear();
        childLasers.Clear();

        for (int i = 0; i < ParentBulletCount; i++)
        {
            float z = (i + 0.5f) * LibraBulletSystem63.ParentBulletSpacing;
            Vector3 pos = LaserSpawnRadius * transform.up.RotateVectorBy(z + 180f);

            var laser = SpawnProjectile(0, z, pos);
            laser.Fire(1f);
            lasers.Add(laser);
        }

        yield return WaitForSeconds(0.5f);

        float spawnRadius = 0f;

        for (int i = 0; i < LaserCount; i++)
        {
            float r = SpawnMaxAngle * Random.Range(0.5f, 1f) * PositiveOrNegativeOne;

            for (int ii = 0; ii < ParentBulletCount; ii++)
            {
                Transform l = lasers[ii].transform;
                float z = l.eulerAngles.z + r;
                Vector3 pos = l.position + (spawnRadius * l.up);

                var laser = SpawnProjectile(1, z, pos, false);
                laser.Fire(FireDelay);
                childLasers.Add(laser);
            }

            spawnRadius += Random.value;
            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < LaserCount; i++)
        {
            for (int ii = 0; ii < ParentBulletCount; ii++)
            {
                Transform l = childLasers[(i * ParentBulletCount) + ii].transform;
                float r = Random.Range(1f, 10f);

                for (int iii = 0; iii < ChildLaserCount; iii++)
                {
                    float z = (iii % 2 * 2 - 1) * 90f + l.eulerAngles.z;
                    Vector3 pos = l.position + (r * l.up);

                    SpawnProjectile(2, z, pos, false).Fire(FireDelay * 0.5f);
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }


        enabled = false;
    }
}