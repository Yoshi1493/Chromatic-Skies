using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing;
            SpawnProjectile(2, z, Vector3.zero).Fire();
        }

        enabled = false;
    }
}