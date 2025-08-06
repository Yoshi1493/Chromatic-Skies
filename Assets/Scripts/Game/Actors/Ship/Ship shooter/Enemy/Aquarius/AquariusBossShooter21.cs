using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBossShooter21 : BossShooter<BossBullet>
{
    const int WaveCount = 5;
    const float WaveSpacing = 1f;
    const int BranchCount = 5;
    const float BranchSpacing = 10f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        for (int i = 1; enabled; i *= -1)
        {
            yield return WaitForSeconds(1f);

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = (i * (ii - ((WaveCount - 1) / 2f)) * WaveSpacing) + ((iii - ((BranchCount - 1) / 2f)) * BranchSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (BranchCount - 1f));

                    SpawnProjectile(1, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}