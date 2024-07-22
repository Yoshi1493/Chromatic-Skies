using System.Collections;
using UnityEngine;

public class LibraBullet51 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);

        StartCoroutine(this.RotateBy(Random.Range(-30f, 30f), 3f));
        yield return this.LerpSpeed(0f, Random.Range(2f, 3f), 1f);
    }
}