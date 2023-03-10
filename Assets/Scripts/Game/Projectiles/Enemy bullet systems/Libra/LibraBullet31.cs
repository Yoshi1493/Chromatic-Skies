using System.Collections;
using UnityEngine;

public class LibraBullet31 : EnemyBullet
{
    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(9f, Random.Range(3f, 4f), 0.6f);
    }
}