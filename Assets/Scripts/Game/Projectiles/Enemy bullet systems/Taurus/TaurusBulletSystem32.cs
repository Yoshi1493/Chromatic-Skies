using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem32 : EnemyShooter<Laser>
{
    const int WaveCount = 3;
    const float WaveSpacing = LaserSpacing / WaveCount;
    const int LaserCount = 4;
    const float LaserSpacing = 360f / LaserCount;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                for (int ii = 0; ii < LaserCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * LaserSpacing);
                    Vector3 pos = PlayerPosition;

                    SpawnProjectile(0, z, pos, false).Fire(ShootingCooldown);
                }
            }
        }
    }
}