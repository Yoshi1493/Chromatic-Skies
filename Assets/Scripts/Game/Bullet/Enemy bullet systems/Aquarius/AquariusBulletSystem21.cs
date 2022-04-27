using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BranchCount = 4;
    const int BranchSpacing = 360 / BranchCount;
    const int BulletCount = 44;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float rand = Random.Range(0f, 360f);

            for (int j = 0; j < BranchCount; j++)
            {
                float z = rand + (j * BranchSpacing);
                SpawnProjectile(1, z, Vector3.zero).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}