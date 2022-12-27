using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 18;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 2.0f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(1, z, pos).Fire();
                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}