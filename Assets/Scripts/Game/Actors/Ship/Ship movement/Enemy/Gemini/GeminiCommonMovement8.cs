using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiCommonMovement8 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = SignX;
        Vector3 p0 = parentShip.transform.position;
        Vector3 p1 = new(p0.x - (d * 8f), 2f);
        Vector3 p2 = p0.RotateVectorBy(180f);

        yield return parentShip.SpiralIntoPoint(p1, 120f, 2f);
        yield return WaitForSeconds(8f);
        yield return parentShip.SpiralIntoPoint(p2, 120f, 2f);

        yield return LeaveScene();
    }
}