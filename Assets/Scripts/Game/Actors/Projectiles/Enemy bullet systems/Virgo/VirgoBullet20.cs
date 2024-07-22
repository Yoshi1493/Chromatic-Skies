using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBullet20 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(0.5f);

        moveDirection *= -1;
        StartCoroutine(this.LerpSpeed(0f, Random.Range(2f, 3f), 1f));
        yield return this.RotateBy(Random.Range(-30f, 30f), MaxLifetime);
    }
}