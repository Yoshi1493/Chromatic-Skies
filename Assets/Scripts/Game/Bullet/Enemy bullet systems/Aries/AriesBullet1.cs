using System.Collections;
using UnityEngine;

public class AriesBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(60f, 3f, Random.value > 0.5f));
        yield return this.LerpSpeed(1f, 3, 1f);
    }
}