using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem51 : EnemyBulletSubsystem<EnemyBullet>
{
    protected override float ShootingCooldown => 1f;

    const int BulletCount = 5;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown * 2f);

        while (enabled)
        {
            for (int i = 0; i < 5; i++)
            {
                float z = PlayerPosition.GetRotationDifference(transform.position) + (i * 360 / BulletCount);
                Vector3 pos = Vector3.zero;

                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}