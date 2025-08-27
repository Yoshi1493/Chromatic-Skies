using System.Collections;
using UnityEngine;

public class PiscesBullet00 : EnemyBullet
{
    protected override float MaxLifetime => 20f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0.5f, 1f);
        StartCoroutine(this.RotateBy(Random.Range(-90f, 90f), 1f));
        StartCoroutine(this.LerpSpeed(0.5f, 1f, 2f));
    }
}