using System.Collections;
using UnityEngine;

public class CapricornBullet51 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3f, 0.5f);
        yield return this.RotateAround(ownerShip, 1f, 60f, rotatesClockwise);
        yield return this.LerpSpeed(MoveSpeed, 3f, 1f);
    }
}