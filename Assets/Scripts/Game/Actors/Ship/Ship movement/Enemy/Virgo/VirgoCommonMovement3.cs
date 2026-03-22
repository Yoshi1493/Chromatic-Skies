using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement3 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 3f, 1.5f);
        yield return WaitForSeconds(4f);
        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(Random.Range(-10f, 10f)), 2f, 8f);

        yield return LeaveScene();
    }
}