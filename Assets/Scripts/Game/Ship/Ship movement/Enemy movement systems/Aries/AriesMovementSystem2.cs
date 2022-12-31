using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class AriesMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f);
    }

    protected override void OnAttackFinish()
    {
        StartMove();
    }
}