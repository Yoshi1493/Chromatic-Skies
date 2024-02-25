using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            Vector3 v1 = 0.8f * new Vector3(-screenHalfWidth, 1f);
            Vector3 v2 = 0.8f * new Vector3(screenHalfWidth, screenHalfHeight);

            List<Vector3> bulletSpawnPositions = GetRandomPointsWithinBounds(v1, v2, BulletCount);
            bulletSpawnPositions = bulletSpawnPositions.OrderBy(i => i.x).ToList();

            for (int i = 0; i < BulletCount; i++)
            {
                float z = 0f;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(0, z, pos);
                bullet.StartCoroutine(bullet.MoveTo(bulletSpawnPositions[i], 1f));
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(9f);
        }
    }
}