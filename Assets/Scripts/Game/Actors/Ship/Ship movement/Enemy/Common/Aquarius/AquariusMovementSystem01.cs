using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusMovementSystem01 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 1.5f, 3f);
        yield return WaitForSeconds(2f);
        yield return parentShip.MoveRelativeLinear(Vector2.one, 3f, 3f);

        yield return WaitForSeconds(2f);
        yield return LeaveScene();
    }
}