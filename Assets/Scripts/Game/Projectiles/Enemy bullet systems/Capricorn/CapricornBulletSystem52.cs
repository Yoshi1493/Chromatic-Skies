using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem52 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 16;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 5f;
    const float BulletSpawnRadius = 2.5f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            Vector3 pos = BulletSpawnRadius * Random.insideUnitCircle;
            float r = RandomAngleDeg;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (ii * BranchSpacing) + ((iii % 2 * 2 - 1) * BulletSpacing) + r;
                    SpawnProjectile(2, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}