using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class TaurusBulletSystem21 : EnemyShooter<Laser>
{
    List<Vector2> spawnPositions = new();

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        spawnPositions.Clear();
        spawnPositions.AddRange(GetRandomPointsAlongBounds(new Vector2(-screenHalfWidth, screenHalfHeight), new Vector2(screenHalfWidth, screenHalfHeight), 2f, 3f));
        spawnPositions.AddRange(GetRandomPointsAlongBounds(new Vector2(screenHalfWidth, -screenHalfHeight), new Vector2(screenHalfWidth, screenHalfHeight), 2f, 3f));
        spawnPositions.Randomize();

        int c = spawnPositions.Count;
        float r = Random.Range(-45f, 45f);

        for (int i = 0; i < c; i++)
        {
            Vector3 pos = spawnPositions[i];

            float z = 0f;

            if (Mathf.Abs(pos.x) == screenHalfWidth)
            {
                z = 90f * Mathf.Sign(pos.x);
            }
            else if (pos.y == screenHalfHeight)
            {
                z = 180f;
            }

            SpawnProjectile(0, z + r, pos, false).Fire(1f);

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}