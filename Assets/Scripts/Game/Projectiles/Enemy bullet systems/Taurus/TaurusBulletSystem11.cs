using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class TaurusBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 128;
    public const float BulletSpacing = 0.64f;
    const float MinDistanceFromShip = 1f;

    List<Vector3> bulletSpawnPositions = new(BulletCount);

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            bulletSpawnPositions = GetRandomPointsWithinBounds(new(-screenHalfWidth, -screenHalfHeight), new(screenHalfWidth, screenHalfHeight), BulletCount);

            for (int i = 0; i < bulletSpawnPositions.Count; i++)
            {
                float x = RoundToNearestMultipleOf(bulletSpawnPositions[i].x, BulletSpacing);
                float y = RoundToNearestMultipleOf(bulletSpawnPositions[i].y, BulletSpacing);
                Vector3 offset = BulletSpacing * 0.5f * Vector3.one;

                Vector3 spawnPos = new Vector3(x, y) - offset;
                bulletSpawnPositions[i] = spawnPos;
            }

            //cull spawn positions
            bulletSpawnPositions = bulletSpawnPositions.Where(p => !p.IsTooClose(PlayerPosition, MinDistanceFromShip)).Distinct().ToList();

            for (int i = 0; i < bulletSpawnPositions.Count; i++)
            {
                float z = 90f;
                Vector3 pos = bulletSpawnPositions[i];
                SpawnProjectile(1, z, pos, false).Fire();
            }

            yield return WaitForSeconds(5f);
        }
    }
}