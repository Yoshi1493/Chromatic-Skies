using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class TaurusBossShooter6 : BossShooter<BossBullet>
{
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 1.0f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 1; i <= transform.childCount; i++)
        {
            SetSubsystemEnabled(i);
        }

        while (enabled)
        {
            for (int i = 0; i < BranchCount; i++)
            {
                float x = (i % 2 * 2 - 1) * (ScreenHalfWidth * 0.8f);
                float y = ScreenHalfHeight * 1.5f;
                float z = 0f;
                Vector3 pos = new(x, y);

                SpawnProjectile(0, z, pos, false).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}