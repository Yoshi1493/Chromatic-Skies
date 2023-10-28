using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 24f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 24;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 1.5f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float t = i * BulletSpacing;
                    float z = t + (ii * BranchSpacing) + (iii * BulletSpacing);
                    Vector3 pos = BulletSpawnRadius * Vector3.up.RotateVectorBy(t);

                    SpawnProjectile(1, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}