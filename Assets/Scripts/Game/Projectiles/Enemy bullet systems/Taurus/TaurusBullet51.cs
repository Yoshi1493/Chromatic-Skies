using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet51 : ScriptableEnemyBullet<TaurusBulletSystem5, EnemyBullet>
{
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

        float z = 0f;
        Vector3 pos = transform.position;

        SpawnBullet(5, z, pos, false).Fire();
    }
}