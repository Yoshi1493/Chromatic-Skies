using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet54 : ScriptableEnemyBullet<TaurusBulletSystem5, EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BranchSpacing / 2f;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const float ShootingCooldown = 0.2f;

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = transform.position;

                SpawnBullet(8, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}