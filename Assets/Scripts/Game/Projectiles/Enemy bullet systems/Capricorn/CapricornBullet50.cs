using System.Collections;
using UnityEngine;

public class CapricornBullet50 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(15f, 5f, rotatesClockwise));
        yield return this.LerpSpeed(4f, 1.5f, 1f);
    }
}