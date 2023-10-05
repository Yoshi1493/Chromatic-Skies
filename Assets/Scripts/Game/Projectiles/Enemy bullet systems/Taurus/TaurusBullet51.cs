using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet51 : ScriptableEnemyBullet<TaurusBulletSystem5, EnemyBullet>
{
    const int WaveCount = 3;
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
            float z = 0f;
            Vector3 pos = transform.position;

            SpawnBullet(5, z, pos, false).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}