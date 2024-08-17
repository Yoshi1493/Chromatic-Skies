using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBullet51 : EnemyBullet
{
    protected override float MaxLifetime => 20f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 0f, 0.5f);
        yield return WaitForSeconds(0.5f);

        StartCoroutine(this.LerpSpeed(0f, 1.5f, 4f));
        yield return this.LerpDirection(Vector3.down, 4f);
    }
}