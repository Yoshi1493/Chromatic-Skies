using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterRed : PlayerSpecialShooter
{
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < BranchCount; i++)
        {
            float z = i * BranchSpacing;
            Vector3 pos = transform.position;

            SpawnProjectile(1, z, pos, false).Fire();
        }

        yield return WaitForSeconds(ShootingCooldown);
        canShoot = true;
    }
}