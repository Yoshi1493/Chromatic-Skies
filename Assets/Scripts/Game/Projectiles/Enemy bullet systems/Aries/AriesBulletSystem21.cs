using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 18;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        int i = 0;

        while (enabled)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(i % 2 + 1, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
            i++;
        }
    }
}