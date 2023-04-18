using System.Collections;
using UnityEngine;

public class LibraBullet40 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3f, 1f);
    }
}