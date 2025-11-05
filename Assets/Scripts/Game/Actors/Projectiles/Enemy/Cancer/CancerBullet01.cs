using System.Collections;
using UnityEngine;

public class CancerBullet01 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
        StartCoroutine(this.RotateBy(Random.Range(-60f, 60f), 2f));
    }
}