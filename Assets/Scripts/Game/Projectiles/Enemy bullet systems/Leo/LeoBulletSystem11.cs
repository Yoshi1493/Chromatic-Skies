using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;
    const float BulletSpacing = 0.8f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            float x = i * BulletSpacing * 0.5f;
            int bulletCount = i + 1;
            
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = ii * BranchSpacing;

                for (int iii = 0; iii < bulletCount; iii++)
                {
                    Vector3 pos = ((iii * BulletSpacing - x) * Vector3.right).RotateVectorBy(z);
                    bulletData.colour = bulletData.gradient.Evaluate(i / (float)WaveCount);

                    SpawnProjectile(2, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}