using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet51 : EnemyBullet
{
    const float CorruptionSquareRadius = 1.96f;

    protected override IEnumerator Move()
    {
        float startSpeed = Random.Range(8f, 12f);
        yield return this.LerpSpeed(startSpeed, 0f, 0.5f);

        yield return WaitForSeconds(1f);

        yield return this.LerpSpeed(0f, 2f, 1f);
        StartCoroutine(this.GraduallyLookAt(playerShip.transform.position, 0.5f));

        CorruptNearbyBullets();

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

    void CorruptNearbyBullets()
    {
        var bullets = transform.parent.GetComponentsInChildren<CancerBullet50>();

        for (int i = 0; i < bullets.Length; i++)
        {
            float sqrDistance = (transform.position - bullets[i].transform.position).sqrMagnitude;

            if (sqrDistance > 0 && sqrDistance < CorruptionSquareRadius)
            {
                if (bullets[i].TryGetComponent(out CancerBullet50 bullet))
                {
                    bullet.Corrupt(sqrDistance);
                }
            }
        }

    }
}