using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBullet12 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0.5f, 1f);
        yield return WaitForSeconds(0.5f);

        StartCoroutine(this.RotateBy(Random.Range(-15f, 15f), 3f));
        yield return this.LerpSpeed(0.5f, 2.5f, 1f);
    }
}