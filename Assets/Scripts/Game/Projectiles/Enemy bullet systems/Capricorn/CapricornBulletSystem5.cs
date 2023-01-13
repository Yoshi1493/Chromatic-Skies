using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 16;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float t = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = 3f * Vector3.right.RotateVectorBy(t);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = t + (iii * BulletSpacing);

                        SpawnProjectile(ii, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            AttackFinishAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}