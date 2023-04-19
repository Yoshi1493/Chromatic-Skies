using System.Collections;
using UnityEngine;

public class LibraBullet40 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return this.LerpSpeed(0f, 3f, 1f);
        yield return this.LerpSpeed(3f, 2.5f, 1f);
    }
}