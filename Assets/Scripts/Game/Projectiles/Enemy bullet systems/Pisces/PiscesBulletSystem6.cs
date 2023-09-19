using System.Collections;
using System.Linq;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class PiscesBulletSystem6 : EnemyShooter<Laser>
{
    const int MinLaserCount = 7;
    const int MaxLaserCount = 13;
    const float LaserSpacing = 10f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            int laserCount = Random.Range(MinLaserCount, MaxLaserCount);

            var xs = GetRandomPointsWithinBounds(new(-screenHalfWidth, screenHalfHeight), new(screenHalfWidth, screenHalfHeight), laserCount)
                .Select(i => i.x * 0.5f)
                .OrderBy(i => i)
                .ToList();

            for (int i = 0; i < laserCount; i++)
            {
                float x = xs[i];
                float y = 1.1f * screenHalfHeight;
                float z = ((i - ((laserCount - 1) / 2f)) * LaserSpacing) + 180f;
                Vector3 pos = new(x, y);

                bulletData.colour = bulletData.gradient.Evaluate(i / (laserCount - 1f));
                SpawnProjectile(0, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(5f);
        }
    }
}