using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 1;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = 0f;
                Vector3 pos = 0.1f * transform.up.RotateVectorBy(z + 90f);

                SpawnProjectile(0, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}