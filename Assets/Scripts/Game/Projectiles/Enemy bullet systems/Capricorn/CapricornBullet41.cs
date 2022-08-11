using System.Collections;
using UnityEngine;

public class CapricornBullet41 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed * 0.5f;

        StartCoroutine(this.RotateBy(10f * MoveSpeed, 1f, rotatesClockwise));
        yield return this.LerpSpeed(MoveSpeed, 0f, 1f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}