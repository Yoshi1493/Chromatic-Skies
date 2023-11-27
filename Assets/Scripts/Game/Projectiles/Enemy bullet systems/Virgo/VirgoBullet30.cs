using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(2f);
        yield return this.LerpSpeed(0f, Random.Range(2f, 3f), 2f);
    }
}