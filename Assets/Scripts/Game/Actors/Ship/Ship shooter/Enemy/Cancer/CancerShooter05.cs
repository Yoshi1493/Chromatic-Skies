using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CancerShooter05 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 18;
    readonly float WaveSpacing = (1f + Mathf.Sqrt(5f)) / 2f * BranchSpacing;
    const int BranchCount = 8;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 1.5f;
    const float SpawnRadiusModifier = 0.3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        while (enabled)
        {
            Vector3 v0 = playerShip.transform.position;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = RandomAngleDeg;
                    float t = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = v0 + ((BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(t));

                    SpawnProjectile(5, z, pos, false).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(15f);
        }
    }
}