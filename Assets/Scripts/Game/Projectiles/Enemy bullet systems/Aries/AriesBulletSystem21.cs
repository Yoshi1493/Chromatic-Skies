using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 18;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = ii * BranchSpacing;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(1, z, pos).Fire();

                z += BranchSpacing * 0.5f;
                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}