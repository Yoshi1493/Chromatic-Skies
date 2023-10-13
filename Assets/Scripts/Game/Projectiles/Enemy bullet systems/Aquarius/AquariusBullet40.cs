using System.Collections;
using UnityEngine;

public class AquariusBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 1f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, Random.Range(6f, 8f), 0.5f);
    }
}