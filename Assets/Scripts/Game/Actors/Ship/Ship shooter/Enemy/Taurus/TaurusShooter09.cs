using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter09 : CommonEnemyShooter<Laser>
{
    const int WaveCount = 10;
    const float WaveSpacing = 360f / WaveCount;
    const int LaserCount = 2;
    const float LaserSpacing = 32f;
    const float BulletSpawnRadius = 0.8f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        float r = transform.position.GetRotationDifference(PlayerPosition);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < LaserCount; ii++)
            {
                int d = ii % 2 * 2 - 1;
                float z = d * ((i * WaveSpacing) + LaserSpacing) + r;
                float t = z + (d * -30f);
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}