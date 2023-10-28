using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet50 : ScriptableEnemyBullet<CancerBulletSystem5, EnemyBullet>
{
    const float WaveSpacing = 3f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 0.5f;
    const float ShootingCooldown = 0.1f;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 1f);
        yield return WaitForSeconds(2f);

        for (int i = 0; enabled; i++)
        {
            Vector3 r = Random.insideUnitCircle;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = -((i * WaveSpacing) + (ii * BranchSpacing));
                Vector3 pos = (BulletSpawnRadius * r).RotateVectorBy(z);

                SpawnBullet(2, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}