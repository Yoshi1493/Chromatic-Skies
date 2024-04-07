using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem41 : EnemyShooter<Laser>
{
    const int WaveCount = 6;
    const int LaserCount = 5;
    const float LaserSpacing = 360f / LaserCount;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            float r = transform.position.GetRotationDifference(PlayerPosition);

            for (int ii = 0; ii < LaserCount; ii++)
            {
                float z = (ii * LaserSpacing) + r;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (LaserCount - 1f));

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}