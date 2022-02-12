using System.Collections;
using UnityEngine;

public class AriesBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            float rand = Random.Range(0.5f, 1f);
            yield return this.LerpSpeed(2f, 4f, rand);
            yield return this.LerpSpeed(4f, 2f, rand);
        }
    }
}