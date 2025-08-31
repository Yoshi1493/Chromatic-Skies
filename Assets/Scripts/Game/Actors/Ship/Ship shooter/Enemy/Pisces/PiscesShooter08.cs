using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter08 : CommonEnemyShooter<Laser>
{
    const int LaserCount = 6;
    const float LaserSpacing = 360f / LaserCount;

    protected override float ShootingCooldown => 6f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float r = transform.position.GetRotationDifference(PlayerPosition);

            for (int ii = 0; ii < LaserCount; ii++)
            {
                float z = (ii * LaserSpacing) + r;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire(1.2f);
            }
        }
    }
}