using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 36f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 12f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing) + 180f;
                Vector3 pos = BulletSpawnRadius * Vector3.up.RotateVectorBy(z);

                SpawnProjectile(2, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }

}