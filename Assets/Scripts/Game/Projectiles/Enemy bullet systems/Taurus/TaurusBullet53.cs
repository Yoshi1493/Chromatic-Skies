using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet53 : ScriptableEnemyBullet<TaurusBulletSystem51, Laser>
{
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

        float z = 180f;
        Vector3 pos = transform.position;

        SpawnBullet(0, z, pos, false).Fire();
    }
}