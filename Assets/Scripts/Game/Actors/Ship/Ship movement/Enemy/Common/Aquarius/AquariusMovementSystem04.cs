using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusMovementSystem04 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 2f, Random.Range(1.5f, 2f));
        yield return WaitForSeconds(1f);
        yield return parentShip.MoveRelative(Vector3.down, 3f, 3f);

        yield return LeaveScene();
    }
}