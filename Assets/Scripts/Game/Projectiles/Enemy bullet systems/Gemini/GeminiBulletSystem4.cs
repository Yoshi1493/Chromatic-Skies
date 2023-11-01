using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 4;
    const float WaveSpacing = BulletSpacing / WaveCount;
    const int BulletCount = 36;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusModifier = 0.5f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.5f;

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        List<(Vector2 pos, float z)> bulletSpawnData = new(WaveCount * BulletCount);

        while (enabled)
        {
            Vector3 v1 = transform.position;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BulletSpacing);
                    Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * Vector3.up.RotateVectorBy(z) + v1;

                    bulletSpawnData.Add((pos, z));
                    SpawnProjectile(0, z, pos, false).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);

            StartMoveAction?.Invoke();

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    var data = bulletSpawnData[0];
                    float z = data.z;
                    float s = BulletBaseSpeed + (ii % 2  * BulletSpeedModifier);
                    Vector3 pos = new(data.pos.x, data.pos.y);

                    bulletData.colour = bulletData.gradient.Evaluate(ii % 2);
                    var bullet = SpawnProjectile(1, z, pos, false);
                    bullet.MoveSpeed = s;
                    bullet.Fire();

                    bulletSpawnData.RemoveAt(0);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            bulletSpawnData.Clear();
            yield return WaitForSeconds(3f);
        }
    }
}