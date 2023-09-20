using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 12;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusMultiplier = 1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        List<EnemyBullet> bullets = new(WaveCount * BranchCount * BulletCount);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float t = i / (WaveCount - 1f);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    Vector3 v1 = transform.up.RotateVectorBy(ii * BranchSpacing);
                    Vector3 v2 = transform.up.RotateVectorBy((ii + 1) * BranchSpacing);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = iii * BulletSpacing;
                        Vector3 pos = Vector3.Lerp(v1, v2, t);

                        bullets.Add(SpawnProjectile(0, z, pos));
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(5f);
        }
    }
}