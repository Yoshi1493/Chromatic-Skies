using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 10f;
    const int BranchCount = 2;
    const int BranchSpacing = 360 / BranchCount;
    const int BulletCount = 3;
    const int BulletSpacing = 360 / BulletCount;

    protected override IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float d = i * WaveSpacing;
                    float t = ii * BranchSpacing;
                    float z = d + t + (iii * BulletSpacing);
                    Vector3 pos = transform.up.RotateVectorBy(d + t);

                    SpawnProjectile(iii + 1, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
            i++;
        }
    }
}