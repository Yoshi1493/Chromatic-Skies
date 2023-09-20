using System.Collections;
using UnityEngine;

public class PiscesBullet60 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2f, 1f);
    }
}