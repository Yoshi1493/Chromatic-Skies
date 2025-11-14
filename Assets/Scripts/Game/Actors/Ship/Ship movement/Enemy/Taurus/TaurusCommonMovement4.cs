using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusCommonMovement4 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelativeLinear(Vector3.down, 2f, 0.25f);
        yield return WaitForSeconds(5f);
        yield return LeaveScene();
    }
}