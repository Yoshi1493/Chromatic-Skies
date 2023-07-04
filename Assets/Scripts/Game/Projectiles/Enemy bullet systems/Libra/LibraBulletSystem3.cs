using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem3 : EnemyShooter<EnemyBullet>
{
    public const float SafeZone = 30f;
    const int WaveCount = 101;
    const float WaveSpacing = 360f / (WaveCount - 1);
    const int BranchCount = ((int)(360f - SafeZone) / (int)BranchSpacing) + 1;
    const float BranchSpacing = 5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);
            SetSubsystemEnabled(2);

            for (int i = 0; i < WaveCount; i++)
            {
                float t = Mathf.Sin(i * WaveSpacing * Mathf.Deg2Rad) * (SafeZone / 4f);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (SafeZone * 0.5f) + (ii * BranchSpacing) + t;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));
                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}