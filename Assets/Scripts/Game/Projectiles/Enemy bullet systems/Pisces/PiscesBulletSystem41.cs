using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem41 : EnemyShooter<Laser>
{
    const int WaveCount = 3;
    const int WaveSpacing = LaserSpacing / WaveCount;
    const int LaserCount = 6;
    const int LaserSpacing = 360 / LaserCount;

    protected override float ShootingCooldown => 1.0f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2.5f);

        while (enabled)
        {
            float r = Random.Range(0f, LaserSpacing);

            for (int i = 0; i < WaveCount; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                for (int j = 0; j < LaserCount; j++)
                {
                    float z = (i * WaveSpacing) + (j * LaserSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }
            }

            enabled = false;
        }
    }
}