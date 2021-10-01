using System.Collections;
using UnityEngine;

public class VirgoBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float goldenRatioRadians = (1 + Mathf.Sqrt(5)) * Mathf.Rad2Deg;

        yield return StartCoroutine(this.LerpSpeed(3f, 0f, 0.5f));

        StartCoroutine(this.LerpSpeed(1f, 3f, 5f));
        StartCoroutine(this.RotateAround(ownerShip, 1f, goldenRatioRadians, delay: 2f));
    }
}