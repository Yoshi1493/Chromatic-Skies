using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBullet00 : EnemyBullet
{
    protected override float MaxLifetime => 20f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0.5f, 0.5f);
        yield return WaitForSeconds(0.5f);
        StartCoroutine(this.RotateBy(Random.Range(-180f, 180f), 1f));
    }
}