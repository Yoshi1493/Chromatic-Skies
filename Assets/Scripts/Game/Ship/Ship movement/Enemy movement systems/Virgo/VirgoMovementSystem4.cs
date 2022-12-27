using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class VirgoMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return this.MoveToRandomPosition(1f, delay: 5f);
        }
    }

    protected override void OnAttackStart(int _)
    {
        StartMove();
    }
}
