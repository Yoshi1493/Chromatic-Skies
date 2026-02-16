using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoCommonMovement4 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(Random.Range(-10f, 10f)), Random.Range(3f, 4f), 1.2f);
        yield return WaitForSeconds(3f);

        yield return LeaveScene();
    }
}