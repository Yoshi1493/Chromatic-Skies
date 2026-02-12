using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 3f, 1.5f);
        yield return WaitForSeconds(5f);

        float d = -SignX;
        Vector3 p1 = new(parentShip.transform.position.x + (d * 5f), -6f);

        yield return parentShip.SpiralIntoPoint(p1, 30f, 4f);
        yield return LeaveScene();
    }
}