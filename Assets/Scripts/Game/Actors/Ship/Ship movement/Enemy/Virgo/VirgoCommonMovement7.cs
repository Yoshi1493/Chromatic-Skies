using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement7 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 1.5f, 2f);
        yield return WaitForSeconds(3f);
        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(Random.Range(-5f, 5f)), 3f, 3f);

        yield return LeaveScene();
    }
}