using System.Collections;
using UnityEngine;

public class AriesBullet21 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(4f, 2f, 1f));
        yield return this.RotateBy(90f, 5f, rotatesClockwise);
    }
}