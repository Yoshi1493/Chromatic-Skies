using System.Collections;
using UnityEngine;

public class VirgoBullet2 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(3f, 1f, 0.5f));
        yield return StartCoroutine(this.LerpSpeed(1f, 3f, 1f));
    }
}