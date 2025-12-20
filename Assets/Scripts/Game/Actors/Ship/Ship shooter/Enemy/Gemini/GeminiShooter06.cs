using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter06 : CommonEnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 16f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const float SpawnRadiusModifier = 0.04f;

    protected override float ShootingCooldown => 0.4f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = i * SpawnRadiusModifier * transform.up.RotateVectorBy(z);

                SpawnProjectile(6, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}