using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 1.0f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        while (enabled)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (i * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}