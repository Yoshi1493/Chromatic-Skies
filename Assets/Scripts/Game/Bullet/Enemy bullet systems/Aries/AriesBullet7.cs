using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBullet7 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(5f, 3f, 2f));
    }
}