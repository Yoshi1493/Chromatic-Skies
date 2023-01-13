using System.Collections;
using UnityEngine;

public class CapricornBullet32 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
        yield return this.RotateBy(60f, 0f, rotatesClockwise, 0.5f);
        MoveSpeed = 2f;
    }
}