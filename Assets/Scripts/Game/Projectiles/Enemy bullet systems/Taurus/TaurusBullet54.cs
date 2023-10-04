using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet54 : ScriptableEnemyBullet<TaurusBulletSystem5, EnemyBullet>
{
    const int BulletCount = 8;
    const float BulletSpacing = 360f / BulletCount;

    protected override float MaxLifetime => 2f;

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

        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing;
            Vector3 pos = transform.position;

            SpawnBullet(7, z, pos, false).Fire();
        }
    }
}