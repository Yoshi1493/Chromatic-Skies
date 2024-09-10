using System.Collections;
using UnityEngine;

public class SagittariusBullet61 : EnemyBullet
{
    [HideInInspector] public EnemyBullet parentBullet;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 3f, 0.5f);
    }
}