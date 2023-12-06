using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 5f;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.15f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        for (int i = 0; enabled; i++)
        {
            float r = i * WaveSpacing;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = r + (ii * BranchSpacing);
                Vector3 pos = transform.up.RotateVectorBy(-r);

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}