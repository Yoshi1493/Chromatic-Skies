using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 20;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = r + (i * BulletSpacing);
                SpawnProjectile(1, z, Vector3.zero).Fire();
            }
        }
    }
}