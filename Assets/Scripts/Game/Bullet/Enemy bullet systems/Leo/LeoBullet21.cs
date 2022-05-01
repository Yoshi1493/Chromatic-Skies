using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeoBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 2f, 1f);
    }
}