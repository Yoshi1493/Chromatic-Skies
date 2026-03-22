using System.Collections;
using UnityEngine;

public class CapricornBullet07 : MinibossBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, Random.Range(2f, 2.5f), 0.5f);
    }
}