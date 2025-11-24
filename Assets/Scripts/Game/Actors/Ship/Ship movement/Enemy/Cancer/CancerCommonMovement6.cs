using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerCommonMovement6 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        float d = SignX;

        yield return parentShip.SpiralIntoPoint(p0 + new Vector3(d * -8f, -1.5f), d * 30f, 2f);
        yield return WaitForSeconds(7f);
        yield return parentShip.SpiralIntoPoint(parentShip.transform.position + new Vector3(d * -2f, 5f), d * 30f, 1.5f);

        yield return LeaveScene();
    }
}