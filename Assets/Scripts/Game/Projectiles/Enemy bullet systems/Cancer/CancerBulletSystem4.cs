using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const float ArcWidth = 300f;
    const int WaveCount = 61;
    const float WaveSpacing = ArcWidth / (WaveCount - 1);
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 1.2f;

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float t = ii * BranchSpacing;
                    float z = (i * WaveSpacing) + t + ((360f - ArcWidth) / 2);
                    Vector3 pos = (BulletSpawnRadius * -transform.up.RotateVectorBy(z) + -transform.up.RotateVectorBy(t));

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));
                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            SetSubsystemEnabled(1);

            yield return WaitForSeconds(5f);
            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}