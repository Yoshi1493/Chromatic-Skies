using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 9;
    const float WaveSpacing = 8f;
    const int BranchCount = 2;
    const int BulletCount = 24;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedMultiplier = 0.2f;

    protected override float ShootingCooldown => 0.15f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float y = (screenHalfHeight - i);
                float s = BulletBaseSpeed - (i * BulletSpeedMultiplier);
                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float x = (ii % 2 * 2 - 1) * ((0.2f + (i * 0.05f)) * screenHalfWidth);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (i * WaveSpacing) + (iii * BulletSpacing);
                        Vector3 pos = new(x, y);

                        var bullet = SpawnProjectile(0, z, pos, false);
                        bullet.MoveSpeed = s;
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(2f);
        }
    }
}