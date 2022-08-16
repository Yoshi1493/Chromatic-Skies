using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 10;
    const float WaveSpacing = BranchSpacing / 2;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;
    const float BulletSpacing = 5f;

    protected override float ShootingCooldown => 0.21f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float t = (i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = transform.up.RotateVectorBy(t);

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = t + (iii * (BulletCount * 0.5f - 0.5f) * BulletSpacing) + ((iii + 1) % 2 * BulletSpacing);
                    SpawnProjectile(2, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}