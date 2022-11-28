using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class PiscesBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 9;
    const float WaveSpacing = 2f;
    const int BranchCount = 20;
    const int BranchSpacing = 360 / BranchCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        int r = PositiveOrNegativeOne;

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = r * ((i * WaveSpacing) + (ii * BranchSpacing));
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 5f);
            r *= -1;
        }
    }
}