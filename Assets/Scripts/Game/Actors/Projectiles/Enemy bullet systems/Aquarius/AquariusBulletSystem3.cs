using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 16;
    const float WaveSpacing = 8f;
    const int BranchCount = 16;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusModifier = 0.8f;

    List<(Vector2 pos, float z)> bulletSpawnData = new(WaveCount * BranchCount);

    protected override float ShootingCooldown => 2 / 60f;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(z);

                bulletSpawnData.Add((pos, z));
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            Vector3 v1 = PlayerPosition;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    int b = (i * BranchCount) + ii;
                    float z = bulletSpawnData[b].z;
                    Vector3 pos = bulletSpawnData[b].pos;

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    if (!pos.IsTooClose(v1 - transform.position))
                    {
                        SpawnProjectile(0, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(4f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(4f);
        }
    }
}