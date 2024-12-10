using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterGreen : PlayerSpecialShooter
{
    const int WaveCount = 3;
    const int MinBranchCount = 12;
    const int BranchCountModifier = 6;
    const float BulletRotationSpeed = 360f;
    const float BulletRotationDuration = 4f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < WaveCount; i++)
        {
            int branchCount = MinBranchCount + (i * BranchCountModifier);
            float branchSpacing = 360f / branchCount;

            for (int ii = 0; ii < branchCount; ii++)
            {
                float z = ii * branchSpacing;
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