using System.Collections;
using UnityEngine;

public class LibraBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();

        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(i, z, pos).Fire();
        }        
    }
}