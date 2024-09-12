using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const float WaveSpacing = BulletSpacing / 2f;
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedModifier = -2f;

    List<EnemyBullet> bullets = new(WaveCount * BulletCount);

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        bullets.Clear();

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BulletSpacing) + r;
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i);

                var bullet = SpawnProjectile(2, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
                bullets.Add(bullet);

            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(1f);

        bullets.ForEach(b => b.Fire());
        enabled = false;
    }
}