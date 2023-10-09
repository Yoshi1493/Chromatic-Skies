using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 3.0f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        while (enabled)
        {
            for (int i = 0; i < BranchCount; i++)
            {
                float x = -screenHalfWidth * 0.8f;
                float y = screenHalfHeight * 1.5f;
                float z = i * BranchSpacing;
                Vector3 pos = new Vector3(x, y).RotateVectorBy(z);

                SpawnProjectile(0, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}