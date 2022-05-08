using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class TaurusBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BulletCount = 256;
    const float minSpacing = 2.25f;

    List<Vector2> spawnPositions = new List<Vector2>(BulletCount);

    bool IsTooClose(Vector3 p) => (ownerShip.transform.position - p).sqrMagnitude < minSpacing || (PlayerPosition - p).sqrMagnitude < minSpacing;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        spawnPositions = GetRandomPointsWithinBounds(new Vector2(-camHalfWidth, -camHalfHeight), new Vector2(camHalfWidth, camHalfHeight), BulletCount);

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            float x = RoundToNearestMultipleOf(spawnPositions[i].x, 0.5f);
            float y = RoundToNearestMultipleOf(spawnPositions[i].y, 0.5f);

            spawnPositions[i] = new Vector2(x, y);
        }

        //cull spawn positions
        spawnPositions = spawnPositions.Where(p => !IsTooClose(p)).Distinct().ToList();
        spawnPositions.ForEach(p => print(p));

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            SpawnProjectile(1, 45f, spawnPositions[i], false);
        }
    }
}