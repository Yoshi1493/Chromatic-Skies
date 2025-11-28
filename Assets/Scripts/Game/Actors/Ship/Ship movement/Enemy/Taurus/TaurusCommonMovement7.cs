using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusCommonMovement7 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        Vector3 p1 = new(-p0.x, 0f);

        yield return parentShip.SpiralIntoPoint(p1, -45f, 1.5f);
        yield return WaitForSeconds(5f);

        yield return LeaveScene();
    }
}