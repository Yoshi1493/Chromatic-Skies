using System.Collections;
using UnityEngine;

public class VirgoBullet41 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return null;
        StartCoroutine(this.LerpSpeed(4f, 2.5f, 1f));
        StartCoroutine(this.RotateBy(120f, 5f, rotatesClockwise, 0.5f));
    }
}