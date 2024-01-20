using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 30;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 1.6f;
    const float BulletBaseSpeed = 1f;

    List<EnemyBullet> bullets = new(BulletCount);

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(4.5f);

        for (int i = 1; enabled; i *= -1)
        {
            bullets.Clear();

            Vector3 v = PlayerPosition;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = i * ii * BulletSpacing;
                Vector3 pos = (BulletSpawnRadius * Vector3.up.RotateVectorBy(z)) + v;

                var bullet = SpawnProjectile(1, z, pos, false);
                bullet.MoveSpeed = 0f;
                bullets.Add(bullet);

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                if (bullets[ii].isActiveAndEnabled)
                {
                    bullets[ii].StartCoroutine(bullets[ii].LerpSpeed(0f, BulletBaseSpeed, 1f));
                    yield return WaitForSeconds(0.2f);
                }
            }

            yield return WaitForSeconds(2f);
        }
    }
}