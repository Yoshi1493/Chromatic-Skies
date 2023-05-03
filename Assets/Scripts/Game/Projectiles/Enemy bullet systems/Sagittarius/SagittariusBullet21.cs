using System.Collections;
using UnityEngine;
using static MathHelper;

public class SagittariusBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(MoveSpeed, 0f, 1f);
        RotateVectorBy(ref moveDirection, (Random.Range(0f, 30f) + 30f) * PositiveOrNegativeOne);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}