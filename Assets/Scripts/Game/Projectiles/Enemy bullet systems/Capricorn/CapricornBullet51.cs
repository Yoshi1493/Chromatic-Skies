using System.Collections;
using UnityEngine;

public class CapricornBullet51 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
        StartCoroutine(this.LerpSpeed(0f, 3f, 1f));
        yield return this.RotateBy(60f, 1f, rotatesClockwise);
    }
}