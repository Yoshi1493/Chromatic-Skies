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

    public void Corrupt(float homingStrength)
    {
        if (corruptionCoroutine == null)
        {
            corruptionCoroutine = _Corrupt(homingStrength);
            StartCoroutine(corruptionCoroutine);
        }
    }

    IEnumerator _Corrupt(float homingStrength)
    {
        StartCoroutine(this.GraduallyLookAt(playerShip.transform.position, Mathf.Max(homingStrength, 0.5f)));

        float currentLerpTime = 0f;
        float totalLerpTime = 1f;

        while (currentLerpTime < totalLerpTime)
        {
            float t = Mathf.Lerp(0f, Mathf.Min(1f, 0.5f / homingStrength), currentLerpTime / totalLerpTime);
            spriteRenderer.color = projectileData.gradient.Evaluate(t);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }

    public override void Destroy()
    {
        corruptionCoroutine = null;
        base.Destroy();
    }
}