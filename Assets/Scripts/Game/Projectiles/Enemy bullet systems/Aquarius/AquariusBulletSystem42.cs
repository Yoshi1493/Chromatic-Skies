using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem42 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 45;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(5f);

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}