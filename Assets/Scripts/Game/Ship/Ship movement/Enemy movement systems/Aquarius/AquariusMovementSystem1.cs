using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class AquariusMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f, delay: 3f);
    }

    protected override void OnAttackFinish()
    {
        StartMove();
    }
}