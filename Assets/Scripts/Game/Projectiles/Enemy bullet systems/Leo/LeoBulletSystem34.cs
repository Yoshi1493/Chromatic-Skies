using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem34 : EnemyShooter<Laser>
{
    const int LaserCount = 8;
    const float LaserSpacing = 360f / LaserCount;

    protected override float ShootingCooldown => 3f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < LaserCount; i++)
            {
                float z = (i * LaserSpacing) + r;
                Vector3 pos = Vector3.zero;
                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}