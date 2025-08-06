using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter07 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 3;

    protected override float ShootingCooldown => 0.2f;
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = 0;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(8, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}