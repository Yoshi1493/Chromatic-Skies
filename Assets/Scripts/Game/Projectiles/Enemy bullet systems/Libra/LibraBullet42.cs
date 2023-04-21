using System.Collections;
using UnityEngine;

public class LibraBullet42 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(6f, 2.5f, 1f));
        yield return this.RotateBy(90f, MaxLifetime, rotatesClockwise);
    }
}