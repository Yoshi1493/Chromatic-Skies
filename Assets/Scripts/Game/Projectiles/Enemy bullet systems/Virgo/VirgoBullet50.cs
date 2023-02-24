using System.Collections;
using UnityEngine;

public class VirgoBullet50 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 0f, 1f);

        StartCoroutine(this.RotateBy(-90f, 0f, rotatesClockwise));
        yield return this.LerpSpeed(4f, 0f, 1f);

        yield return this.RotateBy(90f, 0f, rotatesClockwise);
        MoveSpeed = 3f;
    }
}