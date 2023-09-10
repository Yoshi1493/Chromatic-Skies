using System.Collections;
using UnityEngine;

public class GeminiBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, Random.Range(2f, 4f), 1f);
    }
}