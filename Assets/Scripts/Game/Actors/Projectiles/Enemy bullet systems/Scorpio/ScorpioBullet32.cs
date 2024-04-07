using System.Collections;
using UnityEngine;

public class ScorpioBullet32 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 1.5f, 2f);
    }
}