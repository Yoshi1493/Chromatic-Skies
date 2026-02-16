using System.Collections;
using UnityEngine;

public class LeoCommonMovement3 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        Vector3 p1 = new(0f, p0.y - 1.5f);
        Vector3 p2 = new(-p0.x, p0.y - 3f);

        float d = SignX;

        yield return parentShip.SpiralIntoPointLinear(p1, d * 15f, 2.5f);
        yield return parentShip.SpiralIntoPointLinear(p2, -d * 15f, 2.5f);

        yield return LeaveScene();
    }
}