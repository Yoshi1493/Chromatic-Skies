using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBullet03 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
        yield return WaitForSeconds(1.5f);

        yield return this.RotateBy(Random.Range(-10f, 10f), 2f);
    }
}