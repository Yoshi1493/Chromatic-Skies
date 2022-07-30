using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const float WaveSpacing = 8f;
    const int BulletCount = 20;
    const float BulletSpacing = 360f / BulletCount;

    Stack<EnemyBullet> bullets = new(WaveCount * BulletCount);

    protected override float ShootingCooldown => 0.16f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            Vector3 p = ownerShip.transform.position;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BulletSpacing);
                    float a = (i * 0.5f) + 0.5f;
                    Vector3 pos = a * transform.up.RotateVectorBy(z) + p;

                    var bullet = SpawnProjectile(1, z, pos, false);
                    bullets.Push(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    bullets.Pop().Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(6f);
        }
    }
}