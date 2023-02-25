using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem62 : EnemyShooter<Laser>
{
    const int LaserCount = 6;
    const float LaserSpacing = 360f / LaserCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < LaserCount; i++)
        {
            float r = i * LaserSpacing;
            Vector3 pos = transform.up.RotateVectorBy(r);
            float z = pos.GetRotationDifference(PlayerPosition);

            bulletData.colour = bulletData.gradient.Evaluate(i / (LaserCount - 1f));
            SpawnProjectile(0, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}