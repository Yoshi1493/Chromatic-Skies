using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 18;
    const float WaveSpacing = -180f / WaveCount;
    const int BulletCount = 4;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.25f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(4f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = i * WaveSpacing + 90f;
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed, s, 1f));
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}