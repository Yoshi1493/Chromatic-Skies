using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class VirgoMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return base.Move();

        while (enabled)
        {
            yield return this.MoveToRandomPosition(1f, 1f, 2f, 2f);
        }
    }
}