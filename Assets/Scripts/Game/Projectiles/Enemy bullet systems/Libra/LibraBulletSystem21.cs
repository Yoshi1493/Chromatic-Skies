using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LibraBulletSystem21 : EnemyShooter<Laser>
{
    const float MinLaserSpacing = 1f;
    const float MaxLaserSpacing = 3f;
    const float MinLaserRotation = 5f;
    const float MaxLaserRotation = 30f;

    List<Vector2> spawnPositions = new();

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2f);

            spawnPositions = GetRandomPointsAlongBounds(new Vector2(-screenHalfWidth, screenHalfHeight), new Vector2(screenHalfWidth, screenHalfHeight));

            for (int i = 0; i < spawnPositions.Count; i++)
            {
                int d = PositiveOrNegativeOne;
                float z = Random.Range(MinLaserRotation, MaxLaserRotation) * d + 180f;
                Vector3 pos = spawnPositions[i];

                SpawnProjectile(0, z, pos, false).Fire(1f);

                yield return WaitForSeconds(ShootingCooldown);
            }

            spawnPositions.Clear();

            yield return WaitForSeconds(2f);
        }
    }
}