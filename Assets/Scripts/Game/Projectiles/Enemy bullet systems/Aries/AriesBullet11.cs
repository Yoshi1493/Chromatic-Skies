using System.Collections;
using UnityEngine;

public class AriesBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(45f, 2f, Random.value > 0.5f, delay: 0.5f));
        yield return this.LerpSpeed(4f, 2f, 1f);
    }
}