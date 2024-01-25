using System.Collections;
using UnityEngine;

public class ScorpioBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        moveDirection *= -1;
        yield return this.LerpSpeed(0f, 3f, 2f);
    }
}