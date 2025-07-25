using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesCommonMovement03 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 4.5f, 1f);
        yield return WaitForSeconds(10f);

        yield return LeaveScene();
    }
}