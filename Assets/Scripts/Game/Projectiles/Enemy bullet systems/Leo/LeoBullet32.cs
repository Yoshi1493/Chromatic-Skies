using System.Collections;
using UnityEngine;

public class LeoBullet32 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float endSpeed = Random.Range(1.5f, 2f);
        yield return this.LerpSpeed(4f, endSpeed, 2f);
    }
}