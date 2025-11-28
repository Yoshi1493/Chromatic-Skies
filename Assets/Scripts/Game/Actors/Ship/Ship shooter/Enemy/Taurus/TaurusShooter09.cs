using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter09 : CommonEnemyShooter<Laser>
{
    const int WaveCount = 2;
    const float WaveSpacing = BranchSpacing / 2f;
    const int BranchCount = 10;
    const float BranchSpacing = 360f / BranchCount;
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
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < LaserCount; iii++)
                {
                    float d = ((iii % 2 * 2 - 1) * LaserSpacing) + r;
                    Vector3 pos = v0 + (BulletSpawnRadius * transform.up.RotateVectorBy(d));
                    float t = (i * WaveSpacing) + ((ii + 1) * BranchSpacing);
                    float z = t + pos.GetRotationDifference(v1);
                    pos = (pos - v0).RotateVectorBy(t) + v0;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (BranchCount - 1f));

                    SpawnProjectile(2, z, pos, false).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);
        }
    }
}