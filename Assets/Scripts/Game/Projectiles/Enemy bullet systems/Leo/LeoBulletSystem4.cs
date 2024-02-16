using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem4 : EnemyShooter<Laser>
{
    const int WaveCount = 4;
    const int BranchCount = 2;
    const int LaserCount = 6;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < LaserCount; iii++)
                    {

                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}