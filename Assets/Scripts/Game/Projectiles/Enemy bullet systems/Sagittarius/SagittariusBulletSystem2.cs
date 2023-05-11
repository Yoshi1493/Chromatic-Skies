using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 40;
    const float WaveSpacing = 9f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 2.0f;
    const float SpawnRadiusIncreaseRate = -0.04f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            Vector3 v = transform.up;
            float r = BulletSpawnRadius;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = r * v.RotateVectorBy(z);

                    SpawnProjectile(i % 2, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
                r += SpawnRadiusIncreaseRate;
            }

            yield return WaitForSeconds(3f - ShootingCooldown);
        }
    }
}