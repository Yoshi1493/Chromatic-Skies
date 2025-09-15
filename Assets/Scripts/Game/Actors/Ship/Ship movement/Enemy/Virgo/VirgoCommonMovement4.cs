using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement4 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 3f, 2f);
        yield return WaitForSeconds(3f);
        yield return parentShip.MoveRelative(Vector3.down, 2.5f, 5f);

        yield return LeaveScene();
    }
}