using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = (360 - (int)SafeZone) / (int)WaveSpacing + 1;
    const float WaveSpacing = 8f;
    const float SafeZone = 40f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 90f;
    const float BulletSpawnRadius = 2f;
    const float SpawnRadiusMultiplier = 0.5f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount * BulletCount);

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (SafeZone / 2f) + (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = (BulletSpawnRadius + (ii * SpawnRadiusMultiplier)) * transform.up.RotateVectorBy(z);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        z += iii * BulletSpacing;

                        bulletData.colour = bulletData.gradient.Evaluate(iii);
                        bullets.Add(SpawnProjectile(0, z, pos));
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {

                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(5f);
        }
    }
}