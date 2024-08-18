using System.Collections;
using UnityEngine;

public class ScorpioBullet61 : EnemyBullet
{
    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {        
        yield return this.TransformRotateAround(EnemyMovementBehaviour.originalPosition, MaxLifetime, 6f);
    }
}