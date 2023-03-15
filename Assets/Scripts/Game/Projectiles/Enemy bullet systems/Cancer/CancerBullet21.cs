using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return WaitForSeconds(2f);
        yield return this.LerpSpeed(0f, Random.Range(2f, 3f), 2f);
    }
}