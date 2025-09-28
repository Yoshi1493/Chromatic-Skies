using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class VirgoCommonMovement7 : CommonEnemyMovement
{    protected override IEnumerator Move()
    {
        Vector3 originalPosition = parentShip.transform.position;
        float r = 180f * PositiveOrNegativeOne;

        yield return parentShip.SpiralIntoPoint(originalPosition + (4f * Vector3.down), r, 2f);
        yield return WaitForSeconds(4f);
        yield return parentShip.SpiralIntoPoint(originalPosition, -r, 2f);

        yield return LeaveScene();
    }
}