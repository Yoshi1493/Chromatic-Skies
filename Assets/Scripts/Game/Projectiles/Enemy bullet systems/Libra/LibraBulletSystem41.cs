using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const float WaveSpacing = BranchSpacing / 2f;
    const int BranchCount = 36;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                int b = i % 2 + 2;
                float z = (i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = Vector3.zero;

                SpawnProjectile(b, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}