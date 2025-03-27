using System.Collections;
using UnityEngine;

public class ScorpioBullet32 : BossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 1.5f, 2f);
    }
}