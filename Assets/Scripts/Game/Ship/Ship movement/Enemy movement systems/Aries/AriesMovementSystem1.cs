using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class AriesMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return this.MoveToRandomPosition(2f, 2f, 4f, 3f);
        }
    }

    protected override void OnAttackStart(int _)
    {
        StartMove();
    }
}