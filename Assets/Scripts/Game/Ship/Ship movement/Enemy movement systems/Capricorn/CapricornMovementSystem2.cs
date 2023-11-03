using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        yield return this.MoveToRandomPosition(1f, 2f, 2f);

        yield return this.MoveTo(p0, 1f);

        yield return WaitForSeconds(1f);
        yield return this.MoveToRandomPosition(1f, 3f, 4f);
    }
}