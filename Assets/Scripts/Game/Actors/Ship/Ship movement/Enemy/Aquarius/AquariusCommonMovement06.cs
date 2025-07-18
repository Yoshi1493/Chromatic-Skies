using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusCommonMovement06 : CommonEnemyMovement
{
    [SerializeField] float moveSpeed;

    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, moveSpeed, 2f);

        yield return WaitForSeconds(4f);
        yield return LeaveScene();
    }
}