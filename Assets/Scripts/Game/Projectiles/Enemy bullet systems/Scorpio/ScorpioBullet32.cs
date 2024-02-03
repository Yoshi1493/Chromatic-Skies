using System.Collections;
using UnityEngine;

public class ScorpioBullet32 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 3f, 2f);
    }
}