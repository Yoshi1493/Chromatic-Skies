using System.Collections;
using UnityEngine;

public class SagittariusBullet61 : EnemyBullet
{
    [HideInInspector] public EnemyBullet parentBullet;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 3f, 0.5f);
        //yield return this.RotateAround(parentBullet, 1f, 180f, false);
        //yield return this.LerpSpeed(MoveSpeed, 3f, 1f);
    }
}