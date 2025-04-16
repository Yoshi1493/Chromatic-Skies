using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornMovementSystem2 : BossMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        yield return parentShip.MoveToRandomPosition(1f, 1.5f, 1.5f);

        yield return parentShip.MoveTo(p0, 2f);

        yield return WaitForSeconds(1f);
        yield return parentShip.MoveToRandomPosition(2f, 2f, 3f);
    }
}