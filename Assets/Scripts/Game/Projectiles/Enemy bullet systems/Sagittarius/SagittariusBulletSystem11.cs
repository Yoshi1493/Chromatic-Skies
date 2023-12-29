using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class SagittariusBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const float WaveSpacing = -360f / WaveCount;
    const int BulletCount = 41;
    const float BulletSpawnRadius = 1.5f;

    List<EnemyBullet> bullets = new(WaveCount * BulletCount);
    Vector3[] positions = new Vector3[BulletCount];

    protected override float ShootingCooldown => 0.15f;

    protected override IEnumerator Shoot()
    {
        bullets.Clear();

        for (int i = 0; i < BulletCount; i++)
        {
            positions[i] = BulletSpawnRadius * Random.insideUnitCircle;
        }

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                int b = Mathf.RoundToInt(Random.value) + 1;
                float z = i * WaveSpacing;
                Vector3 pos = positions[ii].RotateVectorBy(z);

                var bullet = SpawnProjectile(b, z, pos);
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

            yield return WaitForSeconds(0.05f);
        }

        enabled = false;
    }
}