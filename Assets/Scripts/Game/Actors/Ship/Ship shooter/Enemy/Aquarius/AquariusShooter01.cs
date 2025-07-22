using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter01 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 3f;

    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = ((iii - (BulletCount - 1) / 2) * BulletSpacing) + (ii * BranchSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(1, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}