using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 10f;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusModifier = 0.5f;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = 0.5f;
    const float NearestSpawnDistance = 1.5f;

    List<(Vector2 pos, float z, bool shouldSpawn)> bulletSpawnData = new(WaveCount * BranchCount);

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        StartMoveAction?.Invoke();

        while (enabled)
        {
            bulletSpawnData.Clear();

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(z) + transform.position;

                    bool shouldSpawn = !pos.IsTooClose(PlayerPosition, NearestSpawnDistance);
                    bulletSpawnData.Add((pos, z, shouldSpawn));

                    if (shouldSpawn)
                    {
                        SpawnProjectile(0, z, pos, false);
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    var data = bulletSpawnData[^1];
                    float z = data.z + 180f;
                    Vector3 pos = new(data.pos.x, data.pos.y);

                    if (data.shouldSpawn)
                    {
                        SpawnProjectile(1, z, pos, false).Fire();
                    }

                    bulletSpawnData.RemoveAt(bulletSpawnData.Count - 1);
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }

            yield return WaitForSeconds(8f);
        }

    }
}