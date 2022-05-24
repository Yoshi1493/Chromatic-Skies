using System.Collections;
using UnityEngine;

public class TaurusBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, -2f, 0.5f);
        yield return this.LerpSpeed(-2f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, 3f, 1f);
    }
}