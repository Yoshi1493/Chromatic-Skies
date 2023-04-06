using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const int BranchCount = 3;
    const float BranchSpacing = 20f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown * 5f);

            for (int i = 0; i < WaveCount; i++)
            {
                float r = PlayerPosition.GetRotationDifference(transform.position);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ((ii - 1) / 2f * BranchSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(1, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}