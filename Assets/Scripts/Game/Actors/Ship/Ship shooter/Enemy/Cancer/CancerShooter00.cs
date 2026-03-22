using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CancerShooter00 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 5;
    const int BulletCount = 12;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.2f);

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = RandomAngleDeg;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.5f);
        }
    }
}