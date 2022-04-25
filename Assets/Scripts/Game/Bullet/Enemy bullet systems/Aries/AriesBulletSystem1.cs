using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const int BranchCount = 6;
    const int BranchSpacing = 360 / BranchCount;
    const int BulletCount = 6;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            for (int i = 0; i < WaveCount; i++)
            {
                float alt = ((i % 2) - 0.5f) * 2;
                float offset = 10f * alt;

                for (int j = 0; j < BranchCount; j++)
                {
                    for (int k = 0; k < BulletCount; k++)
                    {
                        float z = (offset * (j - 1)) + (k * BranchSpacing) + (2.5f * alt);
                        SpawnProjectile(0, z, Vector3.zero).Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown / 2f);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, maxSqrMagDelta: 16f, delay: 2f);
        }
    }
}