using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveWaveCount = 2;
    const int WaveCount = 12;
    const float WaveSpacing = 10f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 8;
    const float BulletSpacing = 15f;
    const float BulletBaseSpeed = 1.5f;
    const float BulletSpeedMultiplier = 0.1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            StartMoveAction?.Invoke();

            yield return WaitForSeconds(1.5f);

            for (int i = 0; i < WaveWaveCount; i++)
            {
                for (int ii = 0; ii < WaveCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        for (int iv = 0; iv < BulletCount; iv++)
                        {
                            float z = (ii * WaveSpacing) + (iii * BranchSpacing) + (iv * BulletSpacing);
                            float s = BulletBaseSpeed + (iv * BulletSpeedMultiplier);
                            Vector3 pos = Vector3.zero;

                            bulletData.colour = bulletData.gradient.Evaluate(iv / (BulletCount - 1f));

                            var bullet = SpawnProjectile(0, z, pos);
                            bullet.MoveSpeed = s;
                            bullet.Fire();
                        }
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(0.5f);
            }
            
            yield return WaitForSeconds(0.5f);

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(10f);
        }
    }
}