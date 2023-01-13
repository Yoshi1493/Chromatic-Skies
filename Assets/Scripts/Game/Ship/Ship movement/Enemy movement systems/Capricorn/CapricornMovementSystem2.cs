using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class CapricornMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            Vector3 startPos = parentShip.transform.position;

            //yield return this.MoveToLinear(startPos + (moveDirection * Vector3.right), 1f);
            yield return WaitForSeconds(1f);
            yield return this.MoveToRandomPosition(1f, 2f, 2f);

            yield return this.MoveToRandomPosition(1f, 2f, 2f, 2f);
        }
    }

    protected override void OnAttackStart(int _)
    {
        StartMove();
    }
}