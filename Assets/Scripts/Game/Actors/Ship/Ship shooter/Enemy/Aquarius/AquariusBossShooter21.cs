using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBossShooter21 : BossShooter<BossBullet>
{
    const int WaveCount = 5;
    const int BranchCount = 5;
    const float BranchSpacing = 10f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(1f);

            for (int i = 0; i < WaveCount; i++)
            {
                float r = PlayerPosition.GetRotationDifference(transform.position);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ((ii - ((BranchCount - 1) / 2f)) * BranchSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(1, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}