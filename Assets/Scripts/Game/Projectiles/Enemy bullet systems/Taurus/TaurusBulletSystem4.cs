using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpacing = 15f;
    const float MaxBulletSpacing = BulletSpacing * 2f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        for (int i = 0; enabled; i++)
        {
            float r = Mathf.PingPong(i, MaxBulletSpacing) - BulletSpacing;
            
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * r) + (ii * BranchSpacing);
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();

                if (z != -z)
                {
                    SpawnProjectile(0, -z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}