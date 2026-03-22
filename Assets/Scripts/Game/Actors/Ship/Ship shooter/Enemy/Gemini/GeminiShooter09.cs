using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter09 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 64;
    const float WaveSpacing = 32f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 0.16f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing) + r;
                    Vector3 pos = (iii % 2 * 2 - 1) * BulletSpacing * Vector3.right;

                    bulletData.colour = bulletData.gradient.Evaluate(iii);

                    SpawnProjectile(9, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}