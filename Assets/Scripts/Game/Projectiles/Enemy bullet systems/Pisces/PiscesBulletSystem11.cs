using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BulletSpacing / 2f;
    const int BulletCount = 24;
    const int BulletSpacing = 360 / BulletCount;

    Stack<EnemyBullet> bullets = new(WaveCount * BulletCount);
    Stack<float> bulletSpeeds = new(WaveCount * BulletCount);

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float r = Random.value;
                bulletSpeeds.Push(r);

                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                float spd = (WaveCount * 3f) - (i * 2f);

                bulletData.colour = bulletData.gradient.Evaluate(r);
                var bullet = SpawnProjectile(1, z, Vector3.zero);
                bullet.StartCoroutine(bullet.LerpSpeed(spd, 0f, 0.5f));
                bullets.Push(bullet);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                var bullet = bullets.Pop();
                bullet.MoveSpeed = bulletSpeeds.Pop();
                bullet.Fire();
            }

            yield return WaitForSeconds(1f);
        }

        enabled = false;
    }
}