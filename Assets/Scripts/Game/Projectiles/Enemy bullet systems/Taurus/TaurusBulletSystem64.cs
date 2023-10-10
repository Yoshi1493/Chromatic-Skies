using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem64 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 1.0f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float x = 2f;
                float y = screenHalfHeight * 1.1f;
                float z = ii * BranchSpacing;
                Vector3 pos = new Vector3(x, y).RotateVectorBy(z);

                SpawnProjectile(3, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}