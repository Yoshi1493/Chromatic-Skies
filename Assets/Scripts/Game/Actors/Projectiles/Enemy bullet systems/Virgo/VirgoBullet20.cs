using System.Collections;
using UnityEngine;

public class VirgoBullet20 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        moveDirection *= -1;
        StartCoroutine(this.LerpSpeed(0f, Random.Range(2f, 3f), 1f));
        yield return this.RotateBy(Random.Range(-30f, 30f), MaxLifetime);
    }
}