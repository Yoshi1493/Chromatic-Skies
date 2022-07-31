using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    const int WaveCount = 35;
    const float WaveSpacing = 360f / (WaveCount + 1);
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.4f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            float r = Random.Range(0f, BranchSpacing);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = -((i * WaveSpacing) + (ii * BranchSpacing)) + r;
                    Vector3 pos = transform.up.RotateVectorBy(z);

                    SpawnProjectile(1, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}