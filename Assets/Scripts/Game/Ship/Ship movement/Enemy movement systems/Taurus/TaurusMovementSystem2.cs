using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class TaurusMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return this.MoveToRandomPosition(1f, 1f, 4f, 2.5f);
        }
    }

    protected override void OnAttackStart(int _)
    {
        StartMove();
    }
}