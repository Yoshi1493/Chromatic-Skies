using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoShooter06 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 16;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 5;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.4f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3.5f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = ii * BranchSpacing;
                        float t = (iii * BulletSpacing) + z;
                        Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                        SpawnProjectile(6, z, pos).Fire();
                    }
                }
            }

            yield return WaitForSeconds(5f);
        }
    }
}