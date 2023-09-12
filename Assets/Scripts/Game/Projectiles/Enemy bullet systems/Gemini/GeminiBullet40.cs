using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 1f;

    protected override IEnumerator Move()
    {
        float currentLerpTime = 0f;

        while (currentLerpTime < MaxLifetime)
        {
            float t = currentLerpTime / MaxLifetime * 2f - MaxLifetime;
            spriteRenderer.color = projectileData.gradient.Evaluate(t);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }
}