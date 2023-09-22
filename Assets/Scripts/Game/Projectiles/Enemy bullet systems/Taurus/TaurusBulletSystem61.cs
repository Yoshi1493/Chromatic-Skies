using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem61 : EnemyShooter<Laser>
{
    const int WaveCount = 15;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const float SpawnAngleVariance = 20f;
    const float LaserSpawnRadius = 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float r = Random.Range(-SpawnAngleVariance, SpawnAngleVariance);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (ii * BranchSpacing) + r;
                Vector3 pos = LaserSpawnRadius * -transform.up.RotateVectorBy(z);

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (ii * BranchSpacing) - r;
                Vector3 pos = LaserSpawnRadius * -transform.up.RotateVectorBy(z);

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}