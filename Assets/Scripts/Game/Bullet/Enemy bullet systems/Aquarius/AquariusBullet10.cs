using System.Collections;
using UnityEngine;

public class AquariusBullet10 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(MoveSpeed, 2f, 2f));
        yield return this.RotateBy(180f, MaxLifetime, rotatesClockwise);
    }
}