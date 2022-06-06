using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BulletCount = 8;
    const int BulletSpacing = 360 / BulletCount;

    protected override float ShootingCooldown => 4f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (i * BulletSpacing) + r;
                SpawnProjectile(1, z, Vector3.zero).Fire();
            }
        }
    }
}