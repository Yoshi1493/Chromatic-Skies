using System.Collections;
using UnityEngine;

public class LibraBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();

        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();
        }
    }
}