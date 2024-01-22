using System.Collections;
using UnityEngine;

public class ScorpioBullet10 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2.5f, ScorpioBulletSystem1.BulletRotationDuration);
    }
}