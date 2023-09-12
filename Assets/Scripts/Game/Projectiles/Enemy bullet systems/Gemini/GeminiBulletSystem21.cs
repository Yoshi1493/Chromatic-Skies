using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 10f;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 1.01f;
    const float SpawnRadiusDecreaseRate = 0.03f;

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                int b = (i % 2) + 2;
                float z = -((i * WaveSpacing) + (ii * BranchSpacing));
                Vector3 pos = (BulletSpawnRadius - (i * SpawnRadiusDecreaseRate)) * transform.up.RotateVectorBy(z);

                SpawnProjectile(b, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}