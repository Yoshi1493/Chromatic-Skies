using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 24;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 2.5f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(2, z, pos).Fire();
            }
        }
    }
}