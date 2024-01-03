using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet51 : EnemyBullet
{
    const float CorruptionRadius = 2.0f;

    protected override IEnumerator Move()
    {
        float startSpeed = Random.Range(8f, 12f);
        yield return this.LerpSpeed(startSpeed, 0f, 0.5f);

        yield return WaitForSeconds(1f);

        StartCoroutine(this.GraduallyLookAt(playerShip.transform.position, 1f));
        yield return this.LerpSpeed(0f, 2f, 1f);

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
        List<CancerBullet50> nearbyBullets = new();

        var bullets = transform.parent.GetComponentsInChildren<CancerBullet50>();

        for (int i = 0; i < bullets.Length; i++)
        {
            if ((transform.position - bullets[i].transform.position).sqrMagnitude < CorruptionRadius)
            {
                nearbyBullets.Add(bullets[i]);
            }
        }

        for (int i = 0; i < nearbyBullets.Count; i++)
        {
            if (nearbyBullets[i].TryGetComponent(out CancerBullet50 bullet))
            {
                bullet.Corrupt();
            }
        }
    }
}