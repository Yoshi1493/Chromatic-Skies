using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CancerShooter06 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 14;
    const int BulletCount = 13;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            yield return WaitForSeconds(6f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = RandomAngleDeg;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(6, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}