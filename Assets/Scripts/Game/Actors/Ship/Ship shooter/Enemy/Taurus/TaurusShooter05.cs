using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter05 : CommonEnemyShooter<Laser>
{
    const int WaveCount = 3;
    const float WaveSpacing = BranchSpacing / WaveCount;
    const int BranchCount = 8;
    const float BranchSpacing = 360f / BranchCount;
    const float LaserSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 1.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    float t = z + 90f;
                    Vector3 pos = LaserSpawnRadius * transform.up.RotateVectorBy(t);

                    SpawnProjectile(1, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(10f);
        }
    }
}