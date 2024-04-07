using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 5f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; enabled; i++)
        {
            Vector3 r = Random.insideUnitCircle;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = (BulletSpawnRadius * r).RotateVectorBy(z);

                SpawnProjectile(1, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}