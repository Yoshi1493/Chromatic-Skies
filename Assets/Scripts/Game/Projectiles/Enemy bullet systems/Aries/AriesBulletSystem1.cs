using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 16;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            SetSubsystemEnabled(1);

            for (int i = 0; i < WaveCount; i++)
            {
                float alt = ((i % 2) - 0.5f) * 2;
                float offset = 10f * alt;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (offset * (ii - 1)) + (iii * BranchSpacing) + (2.5f * alt);
                        SpawnProjectile(0, z, Vector3.zero).Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown / 2f);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, maxSqrMagDelta: 5f, delay: 1f);
        }
    }
}