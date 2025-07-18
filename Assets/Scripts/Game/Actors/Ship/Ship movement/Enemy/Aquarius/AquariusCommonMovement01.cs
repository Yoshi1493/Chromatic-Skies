using System.Collections;
using UnityEngine;

public class AquariusCommonMovement01 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelativeLinear(Vector3.down, 2f, 6f);

        yield return LeaveScene();
    }
}