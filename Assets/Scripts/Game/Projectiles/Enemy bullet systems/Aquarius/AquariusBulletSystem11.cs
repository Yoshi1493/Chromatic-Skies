using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BulletCount = 18;
    const float BulletSpacing = 360f / BulletCount;

    Stack<EnemyBullet> bullets = new(WaveCount * BulletCount);

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        float r = Random.Range(0f, BulletSpacing);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveCount) + (ii * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;
                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(WaveCount - i, 0f, 1f));
                bullets.Push(bullet);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                bullets.Pop().Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}