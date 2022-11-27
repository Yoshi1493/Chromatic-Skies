using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem34 : EnemyShooter<Laser>
{
    const int LaserCount = 8;
    const int LaserSpacing = 360 / LaserCount;

    protected override float ShootingCooldown => 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < LaserCount; i++)
        {
            float z = (i * LaserSpacing) + r;
            Vector3 pos = Vector3.zero;
            SpawnProjectile(0, z, pos).Fire();
        }

        enabled = false;
    }
}