using System.Collections;
using UnityEngine;

public class VirgoBullet20 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(0f, Random.Range(-3.5f, -2f), 1f));
        yield return this.RotateBy(Random.Range(-30f, 30f), MaxLifetime);
    }
}