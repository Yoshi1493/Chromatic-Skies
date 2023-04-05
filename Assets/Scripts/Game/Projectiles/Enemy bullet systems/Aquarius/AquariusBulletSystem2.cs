using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 360f / WaveCount;
    const float WaveAmplitude = 10f;
    const float SafeZone = 30f;
    const int BranchCount = ((int)(360 - (SafeZone * 2)) / (int)BranchSpacing / 2) + 1;
    const float BranchSpacing = 6f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float t = Mathf.Sin(i * WaveSpacing * Mathf.Deg2Rad);
                bulletData.colour = bulletData.gradient.Evaluate(t * 0.5f + 0.5f);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (SafeZone * 0.5f) + (ii * BranchSpacing) + (t * WaveAmplitude);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                    SpawnProjectile(0, z + 180f, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}