using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem22 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BranchCount = 4;
    const int BranchSpacing = 360 / BranchCount;
    const int BulletOffset = 5;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            for (int j = 0; j < BranchCount; j++)
            {
                float z = (i * BulletOffset) + (j * BranchSpacing);
                SpawnProjectile(0, z, Vector3.zero).Fire();

                z += 180f;
                SpawnProjectile(1, z, Vector3.zero).Fire();
            }

            i++;
            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}