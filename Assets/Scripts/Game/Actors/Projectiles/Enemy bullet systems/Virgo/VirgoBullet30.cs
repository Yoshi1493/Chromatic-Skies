using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(2f);
        StartCoroutine(this.LerpSpeed(0f, Random.Range(1.5f, 3.5f), 2f));
        yield return this.RotateBy(Random.Range(-30f, 30f), 2f);
    }
}