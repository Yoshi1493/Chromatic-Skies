using System.Collections;
using UnityEngine;

public class CancerCommonMovement7 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 originalPosition = parentShip.transform.position;
        float d = SignX;

        yield return parentShip.SpiralIntoPoint(originalPosition + new Vector3(d * -2f, -13f), d * 30f, 6f);

        yield return LeaveScene();
    }
}