using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 36;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        StartMoveAction?.Invoke();

        while (enabled)
        {
            for (int i = 0; i < BranchCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * BranchSpacing) + (ii * BulletSpacing);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(i, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}