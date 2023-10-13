using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 1;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float z = 0f;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(1, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}