using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class CapricornMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return this.MoveToRandomPosition(1f, 1f, 2f, 3f);
        }
    }

    protected override void OnAttackStart(int _)
    {
        StartMove();
    }
}