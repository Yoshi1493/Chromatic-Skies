using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet60 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    const float CorruptionChance = 0.06f;

    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(3f, 1.5f, 1f));
     
        if (Random.value <= CorruptionChance)
        {
            StartCoroutine(this.GraduallyLookAt(playerShip.transform.position, 1f));

            float currentLerpTime = 0f, totalLerpTime = 0.5f;

            while (currentLerpTime < totalLerpTime)
            {
                float t = currentLerpTime / totalLerpTime;
                spriteRenderer.color = projectileData.gradient.Evaluate(t);

                yield return EndOfFrame;
                currentLerpTime += Time.deltaTime;
            }
        }

    }
}