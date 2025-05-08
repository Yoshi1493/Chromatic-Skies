using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusMovementSystem02 : CommonEnemyMovement
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveDuration;

    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, moveSpeed, moveDuration);
        yield return WaitForSeconds(2f);
        yield return LeaveScene();
    }
}