using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem32 : EnemyShooter<Laser>
{
    const int WaveCount = 35;
    const float WaveSpacing = 2.5f;
    const int LaserCount = 2;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        Vector3 pos = screenHalfHeight * 1.2f * Vector3.up;

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < LaserCount; ii++)
            {
                float z = (ii % 2 * 2 - 1) * ((i * WaveSpacing) + 90f);
                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                SpawnProjectile(0, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}