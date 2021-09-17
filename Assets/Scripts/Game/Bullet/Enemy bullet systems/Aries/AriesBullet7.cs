using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBullet7 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 5f;

        yield return StartCoroutine(this.LerpSpeed(5f, 2.5f, 2.5f));
    }
}