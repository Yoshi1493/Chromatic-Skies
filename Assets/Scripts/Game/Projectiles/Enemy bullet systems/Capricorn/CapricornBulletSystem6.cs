using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float r = (ii * BranchSpacing);
                    float z = (i * WaveSpacing) + r;
                    Vector3 pos = 3f * Vector3.right.RotateVectorBy(r);

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}