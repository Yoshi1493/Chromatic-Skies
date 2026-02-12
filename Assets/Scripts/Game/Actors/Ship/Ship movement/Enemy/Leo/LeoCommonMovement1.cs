using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 3f, 1.5f);
        yield return WaitForSeconds(5f);

        yield return LeaveScene();
    }
}