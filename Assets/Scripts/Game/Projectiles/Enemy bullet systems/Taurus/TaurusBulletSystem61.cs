using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 1;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 10f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < BranchCount; i++)
            {
                float x = -screenHalfWidth * 0.8f;
                float y = screenHalfHeight * 1.2f;
                float z = i * BranchSpacing;
                Vector3 pos = new Vector3(x, y).RotateVectorBy(z);

                SpawnProjectile(1, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}