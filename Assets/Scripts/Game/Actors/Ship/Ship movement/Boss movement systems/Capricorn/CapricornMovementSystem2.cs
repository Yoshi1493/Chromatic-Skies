using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornMovementSystem2 : BossMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        yield return this.MoveToRandomPosition(1f, 1.5f, 1.5f);

        yield return this.MoveTo(p0, 2f);

        yield return WaitForSeconds(1f);
        yield return this.MoveToRandomPosition(2f, 2f, 3f);
    }
}