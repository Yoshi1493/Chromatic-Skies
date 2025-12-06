using System.Collections;
using UnityEngine;

public class GeminiCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = -SignX;
        Vector3 p0 = parentShip.transform.position;
        Vector3 p1 = p0 + new Vector3(d * 4f, -6f);
        Vector3 p2 = p1 + new Vector3(-d * 14f, 3f) + (Vector3)Random.insideUnitCircle;

        yield return parentShip.SpiralIntoPoint(p1, d * 30f, 1.5f);
        yield return parentShip.SpiralIntoPoint(p2, d * 30f, 4f);

        yield return LeaveScene();
    }
}