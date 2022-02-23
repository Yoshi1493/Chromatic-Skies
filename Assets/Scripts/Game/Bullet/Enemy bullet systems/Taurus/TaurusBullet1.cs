using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaurusBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 1f);
    }
}