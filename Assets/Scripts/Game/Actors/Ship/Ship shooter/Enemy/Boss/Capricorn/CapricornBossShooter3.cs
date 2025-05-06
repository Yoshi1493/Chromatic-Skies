using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBossShooter3 : BossShooter<BossBullet>
{
    const float WaveSpacing = 5f;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (iii % 2 * 2 - 1) * ((i * WaveSpacing) + (ii * BranchSpacing));
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii);
                    SpawnProjectile(0, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}