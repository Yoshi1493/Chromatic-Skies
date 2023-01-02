using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class TaurusMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, delay: 2f);
    }

    protected override void OnAttackFinish()
    {
        StartMove();
    }
}