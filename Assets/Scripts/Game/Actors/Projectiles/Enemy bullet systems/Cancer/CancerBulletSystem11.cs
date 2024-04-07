using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const float ArcHalfWidth = 45f;
    const int RepeatCount = 2;
    const int WaveCount = 30;
    const int BulletCount = 2;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = Random.Range(-ArcHalfWidth, ArcHalfWidth) + 180f;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(2, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}