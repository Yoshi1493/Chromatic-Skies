using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerCommonMovement0 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 3f, 1.2f);
        yield return WaitForSeconds(3f);

        yield return LeaveScene();
    }
}