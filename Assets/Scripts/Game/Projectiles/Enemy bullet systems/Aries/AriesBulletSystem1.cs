using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;
    const float BulletSpacing = 10f;

    protected override float ShootingCooldown => 0.12f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();        

        while (enabled)
        {
            int d = PositiveOrNegativeOne;

            for (int i = 0; i < WaveCount; i++)
            {
                float r = BulletSpacing * d;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (r * (ii - 1)) + (iii * BranchSpacing);
                        Vector3 pos = Vector3.zero;

                        SpawnProjectile(ii % 2, z, pos).Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown * 0.5f);
                }

                yield return WaitForSeconds(ShootingCooldown);
                d *= -1;
            }

            yield return WaitForSeconds(1f);

            SetSubsystemEnabled(1);
            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }
}