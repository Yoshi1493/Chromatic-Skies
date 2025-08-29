using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter02 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 3;
    const int WaveCount = 3;
    const int BranchCount = 3;
    const float BranchSpacing = 15f;

    protected override float ShootingCooldown => 0.15f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < RepeatCount; i++)
        {
            yield return WaitForSeconds(0.4f);

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = ((iii - ((BranchCount - 1) / 2f)) * BranchSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                    SpawnProjectile(2, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}