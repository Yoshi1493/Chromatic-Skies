using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 36;
    const int WaveSpacing = 360 / WaveCount;
    const float WaveSpeedMultiplier = 0.1f;
    const int BulletCount = 2;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedMultiplier = 0.2f;

    List<(float z, float s)> bulletSpawnData = new(WaveCount * BulletCount);

    protected override float ShootingCooldown => 3 / 60f;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i + (ii * 0.5f)) * WaveSpacing;
                float s = (BulletBaseSpeed + (i * WaveSpeedMultiplier)) * (1 - (ii * BulletSpeedMultiplier));
                bulletSpawnData.Add((z, s));
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    int b = (i * BulletCount) + ii;
                    float z = bulletSpawnData[b].z;
                    float s = bulletSpawnData[b].s;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    int b = (i * BulletCount) + ii;
                    float z = 360f - bulletSpawnData[b].z;
                    float s = bulletSpawnData[b].s;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }
}