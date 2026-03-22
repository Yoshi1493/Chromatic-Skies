using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter00 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 3;
    const int WaveCount = 6;
    const int BranchCount = 5;
    const float BranchSpacing = 15f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < RepeatCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = (iii - ((BranchCount - 1) / 2f)) * BranchSpacing + r;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii % 2);

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
        }
    }
}