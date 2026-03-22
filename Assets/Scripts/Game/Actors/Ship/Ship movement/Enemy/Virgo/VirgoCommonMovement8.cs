using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement8 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;

        yield return parentShip.SpiralIntoPoint(p0 + (10f * Vector3.down.RotateVectorBy(-SignX * 60f)), SignX * 30f, 2f);
        yield return WaitForSeconds(5f);

        yield return LeaveScene();
    }
}