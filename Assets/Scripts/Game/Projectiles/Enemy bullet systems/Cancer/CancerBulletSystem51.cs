using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 120;
    const float WaveSpacing = 360 / WaveCount;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                Vector3 r = Random.insideUnitCircle;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = -((i * WaveSpacing) + (ii * BranchSpacing));
                    Vector3 pos = (BulletSpawnRadius * r).RotateVectorBy(z);

                    SpawnProjectile(1, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}