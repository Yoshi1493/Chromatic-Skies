using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerCommonMovement3 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 r = Vector3.down.RotateVectorBy(Random.Range(-30f, 30f));

        parentShip.transform.position += Random.Range(-1f, 1f) * Vector3.up;
        yield return parentShip.MoveRelative(r, 2f, 2f);
        yield return WaitForSeconds(2f);
        yield return parentShip.MoveRelative(r, 2f, 5f);

        yield return LeaveScene();
    }
}