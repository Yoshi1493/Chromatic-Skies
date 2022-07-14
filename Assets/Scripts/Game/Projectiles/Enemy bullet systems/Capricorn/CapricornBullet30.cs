using System.Collections;
using UnityEngine;

public class CapricornBullet30 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
        yield return this.RotateBy(60f, 0f, rotatesClockwise, 0.5f);
        yield return null;
        MoveSpeed = 2.5f;
    }
}