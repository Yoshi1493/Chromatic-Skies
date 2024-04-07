using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 60;
    const float WaveSpacing = 10f;
    const int BranchCount = 2;
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = 0.03f;
    const float BulletSpawnRadius = 0.6f;
    const float SpawnRadiusModifier = -0.01f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float r = RandomAngleDeg;
                    float t = (ii % 2 * 2 - 1) * i * WaveSpacing;

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (iii * BulletSpacing) + r;
                        Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(t);

                        bulletData.colour = bulletData.gradient.Evaluate(ii);

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.MoveSpeed = s;
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(3f);
        }
    }
}