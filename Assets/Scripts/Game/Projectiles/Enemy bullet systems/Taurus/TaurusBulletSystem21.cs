using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class TaurusBulletSystem21 : EnemyShooter<Laser>
{
    List<Vector3> laserSpawnPositions = new();

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            laserSpawnPositions.Clear();
            yield return WaitForSeconds(2f);

            laserSpawnPositions.AddRange(GetRandomPointsAlongBounds(new(-screenHalfWidth, screenHalfHeight), new(screenHalfWidth, screenHalfHeight), 1f, 3f));
            laserSpawnPositions.AddRange(GetRandomPointsAlongBounds(new(screenHalfWidth, -screenHalfHeight), new(screenHalfWidth, screenHalfHeight), 1f, 2f));
            laserSpawnPositions.Randomize();

            float r = Random.Range(-45f, 45f);

            for (int i = 0; i < laserSpawnPositions.Count; i++)
            {
                Vector3 pos = laserSpawnPositions[i];

                float z = r;

                if (Mathf.Abs(pos.x) == screenHalfWidth)
                {
                    z += 90f * Mathf.Sign(pos.x);
                }
                else if (pos.y == screenHalfHeight)
                {
                    z += 180f;
                }

                float t = (laserSpawnPositions.Count - 1 - i) * ShootingCooldown + 0.5f;
                SpawnProjectile(0, z, pos, false).Fire(t);
                SpawnProjectile(0, z + 180f, -pos, false).Fire(t);

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(4f);
        }
    }

    void OnDisable()
    {
        laserSpawnPositions.Clear();
    }
}