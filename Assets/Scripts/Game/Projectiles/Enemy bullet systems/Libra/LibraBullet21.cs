using System.Collections;
using UnityEngine;

public class LibraBullet21 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);

        MoveSpeed = 3f;
        StartCoroutine(this.RotateBy(60f, 0f, rotatesClockwise));
    }
}