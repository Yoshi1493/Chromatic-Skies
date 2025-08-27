using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesCommonMovement0 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 2f, 1.5f);
        yield return WaitForSeconds(5f);
        yield return parentShip.MoveRelative(Vector3.up.RotateVectorBy(Random.Range(-45f, 45f)), 2f, 2f);

        yield return LeaveScene();
    }
}