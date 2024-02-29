using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 128;
    const float WaveSpacing = 12f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 2f / 60;

    protected override IEnumerator Shoot()
    {
        float r = Random.Range(0f, BranchSpacing);
        int d = PositiveOrNegativeOne;

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                int b = Random.Range(0, enemyProjectiles.Count) + 1;
                float z = d * ((i * WaveSpacing) + (ii * BranchSpacing) + r);
                Vector3 pos = Vector3.up.RotateVectorBy(z * 0.5f);

                SpawnProjectile(b, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}