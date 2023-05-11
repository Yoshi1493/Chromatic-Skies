using System.Collections;
using UnityEngine;

public class SagittariusBullet20 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
        yield return this.RotateBy(45f, 0f, rotatesClockwise, 1f);
        yield return this.LerpSpeed(0f, 3f, 1f);
    }
}