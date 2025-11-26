using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter09 : CommonEnemyShooter<Laser>
{
    const int WaveCount = 10;
    const float WaveSpacing = 360f / WaveCount;
    const int LaserCount = 2;
    const float LaserSpacing = 30f;
    const float BulletSpawnRadius = 0.8f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        Vector3 v0 = parentShip.transform.position;
        Vector3 v1 = PlayerPosition;
        float r = transform.position.GetRotationDifference(v1);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < LaserCount; ii++)
            {
                float d = ((ii % 2 * 2 - 1) * LaserSpacing) + r;
                Vector3 pos = v0 + (BulletSpawnRadius * transform.up.RotateVectorBy(d));
                float t = (i + 1) * WaveSpacing;
                float z = t + pos.GetRotationDifference(v1);
                pos = (pos - v0).RotateVectorBy(t) + v0;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                SpawnProjectile(2, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}