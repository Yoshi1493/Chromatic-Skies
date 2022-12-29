using System.Collections;
using UnityEngine;

public class AriesBullet10 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return null;
        StartCoroutine(this.LerpSpeed(2f, 4f, 2f));
        StartCoroutine(this.RotateBy(15f, 4f, rotatesClockwise));
    }
}
