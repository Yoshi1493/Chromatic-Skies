using System.Collections;
using UnityEngine;

public class LeoBullet21 : BossBullet
{
    protected override float MaxLifetime => 9f;

    protected override IEnumerator Move()
    {
        float endSpeed = Random.Range(1.5f, 2.5f);
        yield return this.LerpSpeed(1f, endSpeed, 1f);
    }
}