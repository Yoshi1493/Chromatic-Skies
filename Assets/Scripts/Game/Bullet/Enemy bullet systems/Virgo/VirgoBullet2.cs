using System.Collections;
using UnityEngine;

public class VirgoBullet2 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(3f, 2f, 1f));
        yield return this.RotateBy(135f, MaxLifetime - 1f, rotatesClockwise);
    }
}