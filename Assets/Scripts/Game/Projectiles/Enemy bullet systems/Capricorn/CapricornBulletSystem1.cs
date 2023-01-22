using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const float WaveCount = 360f / WaveSpacing / BranchCount;
    const float WaveSpacing = 10f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        int r = PositiveOrNegativeOne;

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = transform.up.RotateVectorBy(z * r);

                    SpawnProjectile(0, z, pos).Fire();
                    SpawnProjectile(1, -z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}