using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class TaurusBulletSystem21 : EnemyShooter<Laser>
{
    List<Vector3> bulletSpawnPositions = new();

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        bulletSpawnPositions.Clear();
        bulletSpawnPositions.AddRange(GetRandomPointsAlongBounds(new(-screenHalfWidth, screenHalfHeight), new(screenHalfWidth, screenHalfHeight), 2f, 3f));
        bulletSpawnPositions.AddRange(GetRandomPointsAlongBounds(new(screenHalfWidth, -screenHalfHeight), new(screenHalfWidth, screenHalfHeight), 1f, 2f));
        bulletSpawnPositions.Randomize();

        float r = Random.Range(-45f, 45f);

        for (int i = 0; i < bulletSpawnPositions.Count; i++)
        {
            Vector3 pos = bulletSpawnPositions[i];

            float z = r;

            if (Mathf.Abs(pos.x) == screenHalfWidth)
            {
                z += 90f * Mathf.Sign(pos.x);
            }
            else if (pos.y == screenHalfHeight)
            {
                z += 180f;
            }

            SpawnProjectile(0, z, pos, false).Fire();
            SpawnProjectile(0, z + 180f, -pos, false).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }

    void OnDisable()
    {
        bulletSpawnPositions.Clear();
    }
}