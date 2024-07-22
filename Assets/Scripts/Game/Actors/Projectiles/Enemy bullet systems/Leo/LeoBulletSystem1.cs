using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 30;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusModifier = -0.02f;
    const float BulletRotationSpeed = 60f;
    const float BulletRotationSpeedModifier = 2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        int d = iii % 2 * 2 - 1;
                        float z = ii * BranchSpacing;
                        float r = d * (BulletRotationSpeed + (i * BulletRotationSpeedModifier));
                        Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(z);

                        bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy(r, 2f));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(5f);
        }

    }
}