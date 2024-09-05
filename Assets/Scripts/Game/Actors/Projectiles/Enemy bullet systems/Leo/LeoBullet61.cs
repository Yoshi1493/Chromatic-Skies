using System.Collections;
using UnityEngine;

public class LeoBullet61 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 1.5f, 1f);
        yield return this.RotateBy(Random.Range(-90f, 90f), 3f);
    }
}