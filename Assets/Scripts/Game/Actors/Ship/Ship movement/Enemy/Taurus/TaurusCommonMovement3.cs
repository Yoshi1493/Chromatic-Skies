using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusCommonMovement3 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = SignX;

        yield return parentShip.SpiralIntoPoint(parentShip.transform.position + (d * 11f * Vector3.left), d * 30f, 2f);
        yield return WaitForSeconds(6f);
        yield return parentShip.SpiralIntoPoint(parentShip.transform.position + (d * 11f * Vector3.left), d * 30f, 2f);

        yield return LeaveScene();
    }
}