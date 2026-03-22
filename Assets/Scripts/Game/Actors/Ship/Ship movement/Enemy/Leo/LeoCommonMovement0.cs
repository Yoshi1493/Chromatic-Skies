using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoCommonMovement0 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 1f, 1f);
        yield return WaitForSeconds(5f);

        yield return LeaveScene();
    }
}