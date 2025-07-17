using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusMovementSystem02 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 3f, 1.5f);
        yield return WaitForSeconds(15f);
        yield return parentShip.MoveRelative(Vector3.down, 2f, 3f);

        yield return LeaveScene();
    }
}