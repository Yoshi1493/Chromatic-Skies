using System.Collections;
using UnityEngine;

public class AriesBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0.5f, 0.5f);
        StartCoroutine(this.RotateBy(Random.Range(-5f, 5f), 1f));
        yield return this.LerpSpeed(0.5f, 3f, 0.5f);
    }
}