using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const float WaveSpacing = 8f;
    const int BranchCount = 16;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusMultiplier = 0.8f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusMultiplier)) * transform.up.RotateVectorBy(z);
                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(6f);
            StartMoveAction?.Invoke();
            yield return WaitForSeconds(3f);
        }
    }
}