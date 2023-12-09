using System.Collections;
using UnityEngine;

public class TaurusBullet50 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, Random.Range(1.5f, 2.5f), 3f);
    }
}