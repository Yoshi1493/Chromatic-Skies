using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 36;
    const float BulletSpacing = 360f / BulletCount;
    protected override float ShootingCooldown => 0.7f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}