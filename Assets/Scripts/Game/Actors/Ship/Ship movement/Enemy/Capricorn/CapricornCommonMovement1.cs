using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 pos = parentShip.transform.position;
        float d = -Mathf.Sign(pos.x);

        yield return parentShip.MoveRelative(Vector3.down, 3f, 1.2f);
        yield return WaitForSeconds(12f);
        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(d * 45f), 2f, 6f);

        yield return LeaveScene();
    }
}