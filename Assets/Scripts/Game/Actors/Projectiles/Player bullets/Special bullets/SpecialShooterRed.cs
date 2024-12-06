using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterRed : PlayerSpecialShooter
{
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 8;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 540f;
    const float BulletRotationDuration = 4f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(1, z, pos);
                bullet.StartCoroutine(bullet.RotateBy((i % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(SpecialCooldown);
        canShoot = true;
    }
}