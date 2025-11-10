using System.Collections;
using UnityEngine;

public class TaurusCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(-SignX * 15f), 4f, 3f);

        yield return LeaveScene();
    }
}