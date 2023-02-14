using System.Collections;
using UnityEngine;

public class LeoBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(60f, 5f, Random.value > 0.5f,  delay: 0.5f));
        StartCoroutine(this.LerpSpeed(1.5f, 4f, 1f));
        yield return this.LerpSpeed(4f, 1.5f, 2f);
    }
}