using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = -SignX;
        Vector3 p0 = parentShip.transform.position;
        Vector3 p1 = p0 + new Vector3(d * 4f, -6f);
        Vector3 p2 = p1 + new Vector3(-d * 4f, 3f);

        yield return parentShip.SpiralIntoPoint(p1, d * 30f, 1f);
        yield return parentShip.SpiralIntoPoint(p2, d * 30f, 1f);
        yield return WaitForSeconds(2f);
        yield return parentShip.MoveRelative(Vector3.down, 5f, 1.8f);

        yield return LeaveScene();
    }
}