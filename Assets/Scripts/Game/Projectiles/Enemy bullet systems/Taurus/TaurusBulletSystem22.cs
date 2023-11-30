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
    const int BulletCount = 5;
    const float BulletSpacing = 5f;

    List<EnemyBullet> bullets = new(RepeatCount * WaveCount);

    protected override float ShootingCooldown => 2f / 60;

    protected override IEnumerator Shoot()
    {
        bullets.Clear();        

        for (int i = 0; i < RepeatCount; i++)
        {
            Vector3 v1 = transform.position;
            yield return WaitForSeconds(ShootingCooldown);

            Vector3 v2 = (ownerShip.transform.position - v1).normalized.RotateVectorBy(Random.Range(-15f, 15f));
            Vector3 v3 = v1 + (3f * v2);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                Vector3 pos = Vector3.Lerp(v1, v3, (ii / (WaveCount - 1f)));

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (ii * WaveSpacing) + ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing);
                    SpawnProjectile(1, z, pos, false).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.1f);
        }

        enabled = false;
    }
}