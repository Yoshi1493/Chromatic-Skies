using System.Collections;
using UnityEngine;
using static MathHelper;

public class SagittariusBullet40 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2f, 0.5f);
        yield return this.RotateBy(Random.Range(15f, 30f) * PositiveOrNegativeOne, 3f);
    }
}