using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class SagittariusBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const float WaveSpacing = 360f / WaveCount;
    const int BulletCount = 41;
    const float BulletSpawnRadius = 1.5f;

    Vector3[] positions = new Vector3[BulletCount];
    List<EnemyBullet> bullets = new(WaveCount * BulletCount);

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            positions[i] = BulletSpawnRadius * Random.insideUnitCircle;
        }

        for (int i = 0; i < WaveCount; i++)
        {
            float z = i * -WaveSpacing;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                Vector3 pos = positions[ii].RotateVectorBy(z);

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.Fire();
                bullets.Add(bullet);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        for (int i = 0; i < BulletCount; i++)
        {
            float r = RandomAngleDeg;

            for (int ii = 0; ii < WaveCount; ii++)
            {
                int b = ii * BulletCount + i;

                RotateVectorBy(ref bullets[b].moveDirection, r);
                StartCoroutine(bullets[b].LerpSpeed(0f, 2f, 1f));
            }

            yield return WaitForSeconds(ShootingCooldown * 0.5f);
        }

        bullets.Clear();
        enabled = false;
    }
}