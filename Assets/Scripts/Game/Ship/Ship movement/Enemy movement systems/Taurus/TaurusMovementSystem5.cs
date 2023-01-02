using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class TaurusMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, delay: 1f);
    }

    protected override void OnAttackFinish()
    {
        StartMove();
    }
}