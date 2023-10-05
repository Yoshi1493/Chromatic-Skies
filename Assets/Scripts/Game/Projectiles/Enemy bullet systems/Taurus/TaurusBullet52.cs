using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet52 : ScriptableEnemyBullet<TaurusBulletSystem5, EnemyBullet>
{
    const int WaveCount = 2;
    const float WaveSpacing = BranchSpacing / WaveCount;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const float ShootingCooldown = 0.5f;

    protected override float MaxLifetime => 2.5f;

    protected override IEnumerator Move()
    {
        float currentLerpTime = 0f, totalLerpTime = 0.5f;

        while (currentLerpTime < totalLerpTime)
        {
            float t = currentLerpTime / totalLerpTime;
            spriteRenderer.color = projectileData.gradient.Evaluate(t);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + ((ii + 0.5f) * BranchSpacing);
                Vector3 pos = transform.position;

                SpawnBullet(6, z, pos, false).Fire();

            }

            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}