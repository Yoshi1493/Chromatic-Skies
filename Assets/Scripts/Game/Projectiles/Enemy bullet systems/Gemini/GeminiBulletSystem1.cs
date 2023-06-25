using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class GeminiBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float r = RandomAngleDeg;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float t = (i * WaveSpacing) + (ii * BranchSpacing);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = t + ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing) + r;
                        Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                        SpawnProjectile(0, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(4f);

            StartMoveAction?.Invoke();

            yield return WaitForSeconds(1f);
        }
    }
}