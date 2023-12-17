using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class GeminiBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 16;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 8;
    const float BulletSpacing = 2.5f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float r = RandomAngleDeg;
                Vector3 pos = transform.up.RotateVectorBy(RandomAngleDeg);

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (ii * BranchSpacing) + (iii * BulletSpacing) + r;
                    SpawnProjectile(1, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}