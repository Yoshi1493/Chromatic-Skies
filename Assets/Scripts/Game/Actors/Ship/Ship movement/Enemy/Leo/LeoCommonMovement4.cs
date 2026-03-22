using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoCommonMovement4 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        Vector3 p1 = new(0f, 1f);
        Vector3 p2 = new(-p0.x, p0.y);

        float d = -SignX;
        float r = Random.Range(15f, 45f) * d;

        yield return parentShip.SpiralIntoPoint(p1, r, 2f);
        yield return WaitForSeconds(10f);
        yield return parentShip.SpiralIntoPoint(p2, r, 2f);

        yield return LeaveScene();
    }
}