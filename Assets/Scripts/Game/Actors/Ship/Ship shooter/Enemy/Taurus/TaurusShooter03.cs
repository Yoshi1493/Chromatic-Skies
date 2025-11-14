using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 3;
    const int WaveCount = 5;
    const float WaveSpacing = 2f;
    const float BranchCount = 24;
    const float BranchSpacing = 360f / BranchCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < RepeatCount; i++)
        {
            yield return WaitForSeconds(1f);

            float r = Random.Range(0f, BranchSpacing);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = (ii * WaveSpacing) + (iii * BranchSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(2, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}