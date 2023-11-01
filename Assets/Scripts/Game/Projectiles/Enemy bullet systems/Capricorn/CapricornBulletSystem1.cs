using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveWaveCount = 2;
    const int WaveCount = 10;
    const float WaveSpacing = 360f / BranchCount / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 8;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            StartMoveAction?.Invoke();

            for (int i = 0; i < WaveWaveCount; i++)
            {
                for (int ii = 0; ii < WaveCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        for (int iv = 0; iv < BulletCount; iv++)
                        {
                            float t = (i % 2 * 2 - 1) * ((ii * WaveSpacing) + (iii * BranchSpacing));
                            float z = t + (iv * BulletSpacing);
                            Vector3 pos = transform.up.RotateVectorBy(t);

                            bulletData.colour = bulletData.gradient.Evaluate(iii);
                            SpawnProjectile(0, z, pos).Fire();
                        }
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(1f);
            }

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(2f);
        }
    }
}