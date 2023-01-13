using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class CapricornMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, delay: 5f);
    }

    protected override void OnAttackFinish()
    {
        StartMove();
    }
}