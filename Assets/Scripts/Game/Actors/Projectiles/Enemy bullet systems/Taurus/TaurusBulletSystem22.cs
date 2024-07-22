using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem22 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 4;
    const float RepeatSpacing = 75f;
    const int WaveCount = 15;
    const float WaveSpacing = 360f / WaveCount;
    const int BulletCount = 3;
    const float BulletSpacing = 5f;

    List<EnemyBullet> bullets = new(RepeatCount * WaveCount);

    protected override float ShootingCooldown => 2f / 60;

    protected override IEnumerator Shoot()
    {
        bullets.Clear();        

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (ii * WaveSpacing) + ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(1, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.1f);
        }

        enabled = false;
    }
}