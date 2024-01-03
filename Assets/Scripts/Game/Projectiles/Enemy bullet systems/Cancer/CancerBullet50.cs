using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet50 : EnemyBullet
{
    IEnumerator corruptionCoroutine;
    const float CorruptionChance = 0.05f;

    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(3f, 2f, 1f));
    }

    public void Corrupt(float rotationAmount)
    {
        if (corruptionCoroutine != null)
        {
            StopCoroutine(corruptionCoroutine);
        }

        corruptionCoroutine = _Corrupt(rotationAmount);
        StartCoroutine(corruptionCoroutine);
    }

    IEnumerator _Corrupt(float rotationAmount)
    {
        StartCoroutine(this.RotateBy(rotationAmount, 1f, false));

        float currentLerpTime = 0f;
        float totalLerpTime = 1f;

        while (currentLerpTime < totalLerpTime)
        {
            float t = currentLerpTime / totalLerpTime;
            spriteRenderer.color = projectileData.gradient.Evaluate(t);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }
}