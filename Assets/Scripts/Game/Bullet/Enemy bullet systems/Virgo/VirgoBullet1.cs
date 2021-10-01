using System.Collections;
using UnityEngine;

public class VirgoBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float goldenRatioRadians = ((1 + Mathf.Sqrt(5)) / 2) * Mathf.Rad2Deg;

        yield return StartCoroutine(this.LerpSpeed(3f, 1f, 0.5f));

        StartCoroutine(this.LerpSpeed(1f, 3f, 2f));
    }
}