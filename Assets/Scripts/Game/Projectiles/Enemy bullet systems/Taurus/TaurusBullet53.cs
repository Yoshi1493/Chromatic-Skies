using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet53 : ScriptableEnemyBullet<TaurusBulletSystem5, EnemyBullet>
{
    const int WaveCount = 5;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const float ShootingCooldown = 0.2f;

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = ii * BranchSpacing;
                Vector3 pos = transform.position;

                SpawnBullet(7, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}