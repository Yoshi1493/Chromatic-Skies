using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(1f);
            yield return this.MoveToRandomPosition(1f, 2f, 2f);

            yield return this.MoveToRandomPosition(1f, 2f, 2f, 2f);
        }
    }
}