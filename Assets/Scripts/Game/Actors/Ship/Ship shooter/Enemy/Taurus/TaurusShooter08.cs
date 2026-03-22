using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter08 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 10;
    const float WaveSpacing = 360f / BranchCount / WaveCount;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing) + r;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(6, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}