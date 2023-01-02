using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class TaurusMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            yield return this.MoveToRandomPosition(1f, 1f, 2f, 2f);
        }
    }

    protected override void OnAttackStart(int _)
    {
        StartMove();
    }
}