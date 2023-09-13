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
    const float SpawnRadiusMultiplier = 0.5f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedMultiplier = 0.5f;

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        List<(Vector3 xy, float z)> bulletPosRotData = new(WaveCount * BulletCount);

        while (enabled)
        {
            Vector3 v1 = transform.position;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BulletSpacing);
                    Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusMultiplier)) * Vector3.up.RotateVectorBy(z) + v1;

                    bulletPosRotData.Add((pos, z));
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
                    var xyz = bulletPosRotData[0];
                    float z = xyz.z;
                    float s = BulletBaseSpeed + (ii % 2  * BulletSpeedMultiplier);
                    Vector3 pos = new(xyz.xy.x, xyz.xy.y);

                    bulletData.colour = bulletData.gradient.Evaluate(ii % 2);
                    var bullet = SpawnProjectile(1, z, pos, false);
                    bullet.MoveSpeed = s;
                    bullet.Fire();

                    bulletPosRotData.RemoveAt(0);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            bulletPosRotData.Clear();
            yield return WaitForSeconds(3f);
        }
    }
}