using System.Collections;
using UnityEngine;

public class AriesBullet10 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return null;
        StartCoroutine(this.LerpSpeed(5f, 2.5f, 0.5f));
        StartCoroutine(this.RotateBy(15f, 4f, rotatesClockwise));
    }
}
