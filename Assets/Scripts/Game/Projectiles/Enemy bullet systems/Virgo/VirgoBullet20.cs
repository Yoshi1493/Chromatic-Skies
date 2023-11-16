using System.Collections;
using UnityEngine;

public class VirgoBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        moveDirection *= -1;
        StartCoroutine(this.LerpSpeed(0f, Random.Range(2f, 3.5f), 1f));
        yield return this.RotateBy(Random.Range(-30f, 30f), MaxLifetime);
    }
}