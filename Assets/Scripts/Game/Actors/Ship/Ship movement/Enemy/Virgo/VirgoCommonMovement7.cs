using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class VirgoCommonMovement7 : CommonEnemyMovement
{    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        float r = 180f * PositiveOrNegativeOne;

        yield return parentShip.SpiralIntoPoint(p0 + (4f * Vector3.down), r, 2f);
        yield return WaitForSeconds(4f);
        yield return parentShip.SpiralIntoPoint(p0, -r, 2f);

        yield return LeaveScene();
    }
}