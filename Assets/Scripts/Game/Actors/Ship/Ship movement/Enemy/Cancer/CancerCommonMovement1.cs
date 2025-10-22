using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.SpiralIntoPoint(parentShip.transform.position + 4f * Vector3.down.RotateVectorBy(-SignX * 10f), SignX * 45f, 1.5f);
        yield return WaitForSeconds(5f);

        yield return LeaveScene();
    }
}