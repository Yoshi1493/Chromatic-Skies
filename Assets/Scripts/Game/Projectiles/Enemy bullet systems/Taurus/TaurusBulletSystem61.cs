using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 5;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float x = 3f;
                    float y = screenHalfHeight * 1.1f;
                    float z = ii * BranchSpacing;
                    Vector3 pos = new Vector3(x, y).RotateVectorBy(z);

                    SpawnProjectile(1, z, pos, false).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
        }
    }
}