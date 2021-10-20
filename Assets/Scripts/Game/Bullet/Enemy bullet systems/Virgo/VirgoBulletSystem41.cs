using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem41 : EnemyShooter<EnemyBullet>
{
    Queue<EnemyBullet> bullets = new Queue<EnemyBullet>(16);

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 1; i < 9; i++)
            {
                Vector3 spawnPos = 8f * Vector3.left.RotateVectorBy(i * 15f) + new Vector3(5f, 7f);
                bullets.Enqueue(SpawnProjectile(8, 0f, spawnPos, false));

                spawnPos = 8f * Vector3.right.RotateVectorBy(i * -15f) + new Vector3(-5f, 7f);
                bullets.Enqueue(SpawnProjectile(8, 0f, spawnPos, false));

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            while (bullets.Count > 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    var bullet = bullets.Dequeue();

                    for (int j = 0; j < 6; j++)
                    {
                        float z = j * 60f;
                        SpawnProjectile(0, z, bullet.transform.position, false).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);
        }
    }
}