using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CancerShooter00 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 60;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.2f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = RandomAngleDeg;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}