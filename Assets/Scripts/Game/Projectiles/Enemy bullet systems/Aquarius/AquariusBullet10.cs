using System.Collections;
using UnityEngine;

public class AquariusBullet10 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed * 0.5f;
        StartCoroutine(this.LerpSpeed(MoveSpeed, endSpeed, 3f));
        yield return this.RotateBy(90f, MaxLifetime, rotatesClockwise);
    }
}