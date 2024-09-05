using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem62 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 16;
    const float WaveSpacing = 360f / WaveCount;
    const int BulletCount = 18;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 45f;
    const float BulletRotationDuration = 5f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(3, z, pos);
                bullet.StartCoroutine(bullet.RotateBy((ii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
                bullet.Fire();
            }
        }

        enabled = false;
    }
}