using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusMovementSystem00 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 2.5f, 2f);
        yield return WaitForSeconds(3f);

        yield return LeaveScene();
    }
}