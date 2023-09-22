using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem62 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const float WaveSpacing = 3f;
    const int BranchCount = 40;
    const float MaxSafeZone = 70f;
    const float BulletBaseSpeed = 6f;
    const float BaseSpeedMultiplier = 0.1f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i <= WaveCount; i++)
        {
            float safeZone = MaxSafeZone - (i * WaveSpacing);
            float branchSpacing = (360 - safeZone) / BranchCount;

            for (int ii = 0; ii <= BranchCount; ii++)
            {
                float z = (safeZone / 2f) + (ii * branchSpacing);
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