using System.Collections;
using UnityEngine;

public class LeoBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float randSpeed = Random.Range(1.5f, 2.5f);
        yield return this.LerpSpeed(1f, randSpeed, 1f);
    }
}