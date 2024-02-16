using System.Collections;
using UnityEngine;

public class LeoBullet32 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = Random.Range(1.5f, 2.5f);
        yield return this.LerpSpeed(4f, endSpeed, 2f);
    }
}