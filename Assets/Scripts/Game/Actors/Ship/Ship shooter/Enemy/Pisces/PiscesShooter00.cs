using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter00 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 4;
    const int WaveCount = 18;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = (i * 0.5f * BranchSpacing) + (iii * BranchSpacing);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}