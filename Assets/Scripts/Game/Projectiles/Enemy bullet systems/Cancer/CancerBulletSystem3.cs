using System.Collections;
using UnityEngine;

public class CancerBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 16;
    const float BulletSpacing = 360f / BulletCount;
    public const float SpawnRadius = 2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < BulletCount; i++)
        {
            int b = i == 0 ? 0 : i % 4 == 0 ? 1 : 2;
            float z = i * BulletSpacing;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(b, z, pos).Fire();
        }

        yield break;
    }
}