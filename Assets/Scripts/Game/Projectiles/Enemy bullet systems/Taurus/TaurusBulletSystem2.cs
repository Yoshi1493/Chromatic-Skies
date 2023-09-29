using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 18;
    const float WaveSpacing = 180f / WaveCount;
    const int BranchCount = 2;
    const float MinBranchSpacing = 15f;
    const float MaxBranchSpacing = 45f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            for (int i = WaveCount - 1; i >= 0; i--)
            {
                float r = Random.Range(MinBranchSpacing, MaxBranchSpacing);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * r);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();

                    if (i != 0)
                    {
                        SpawnProjectile(0, -z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}