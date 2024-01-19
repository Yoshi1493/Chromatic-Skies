using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BulletCount = 30;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.4f;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 9f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing;
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f));
                bullet.StartCoroutine(bullet.RotateBy((i % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
            }
        }

        enabled = false;
    }
}