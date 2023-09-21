using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem62 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = (int)((MaxSafeZone - MinSafeZone) / WaveSpacing) + 1;
    const float WaveSpacing = 5f;
    const float BranchSpacing = 5f;
    const float MinSafeZone = 10f;
    const float MaxSafeZone = 75f;
    const float BulletBaseSpeed = 6f;
    const float BaseSpeedMultiplier = 0.1f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            float safeZone = MaxSafeZone - (i * WaveSpacing);
            int branchCount = (int)((360f - safeZone) / BranchSpacing) + 1;

            for (int ii = 0; ii < branchCount; ii++)
            {
                float z = (safeZone / 2f) + (ii * BranchSpacing);
                float s = BulletBaseSpeed - (i * BaseSpeedMultiplier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));
                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}