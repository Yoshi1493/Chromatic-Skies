using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet50 : EnemyBullet
{
    IEnumerator corruptionCoroutine;

    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(3f, 2f, 1f));
    }

    public void Corrupt()
    {
        if (corruptionCoroutine != null)
        {
            StopCoroutine(corruptionCoroutine);
        }

        corruptionCoroutine = _Corrupt();
        StartCoroutine(corruptionCoroutine);
    }

    IEnumerator _Corrupt()
    {
        StartCoroutine(this.GraduallyLookAt(playerShip.transform.position, 2f));

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