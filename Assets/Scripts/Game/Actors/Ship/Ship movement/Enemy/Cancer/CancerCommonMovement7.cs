using System.Collections;
using UnityEngine;

public class CancerCommonMovement7 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        float d = SignX;

        yield return parentShip.SpiralIntoPoint(p0 + new Vector3(-d * 2f, -13f), d * 30f, 6f);

        yield return LeaveScene();
    }
}