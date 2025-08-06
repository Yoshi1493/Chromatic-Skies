using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusCommonMovement07 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(Random.Range(-15f, 15f)), Random.Range(2.5f, 4f), 1.2f);
        yield return WaitForSeconds(1f);
        yield return parentShip.MoveRelative(Vector3.down, 3f, 3f);

        yield return LeaveScene();
    }
}