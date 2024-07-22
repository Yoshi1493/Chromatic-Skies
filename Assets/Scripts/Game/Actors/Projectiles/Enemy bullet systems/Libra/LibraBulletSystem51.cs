using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LibraBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 99;
    const float WaveSpacing = 5f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpawnRadius = 1f;


    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float t = (i * WaveSpacing) + ((ii + 0.5f) * BranchSpacing);

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = RandomAngleDeg * 0.5f - 90f;
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                    SpawnProjectile(1, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}