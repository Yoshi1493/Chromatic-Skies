using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem41 : EnemyShooter<Laser>
{
    const int WaveCount = 4;
    const int LaserCount = 8;
    const float LaserSpacing = 360f / LaserCount;
    const float LaserSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < LaserCount; ii++)
            {
                float z = (ii * LaserSpacing) + r;
                Vector3 pos = LaserSpawnRadius * Vector3.up.RotateVectorBy(z);

                bulletData.colour = bulletData.gradient.Evaluate(i % 2);
                SpawnProjectile(0, z, pos).Fire();
            }
        }

        enabled = false;
    }
}