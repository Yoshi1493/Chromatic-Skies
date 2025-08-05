using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesCommonMovement01 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 4.5f, 1f);
        yield return WaitForSeconds(6f);

        yield return LeaveScene();
    }
}