using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 3;
    const int BranchSpacing = 360 / BranchCount;
    const float BulletSpacing = 15f;
    const float MaxBulletSpacing = BulletSpacing * 2f;

    protected override float ShootingCooldown => 0.03f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        int i = 0;

        while (enabled)
        {
            float r = Mathf.PingPong(i, MaxBulletSpacing) - BulletSpacing;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (ii * BranchSpacing) + (i * r) + 180f;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            i = (int)Mathf.Repeat(++i, MaxBulletSpacing * 2);
        }
    }
}