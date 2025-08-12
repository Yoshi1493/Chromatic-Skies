using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornCommonMovement4 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector2 moveDirection = new Vector2(SignX * -4f, -3f).normalized;
        yield return parentShip.MoveRelative(moveDirection, 3f, 2f);
        yield return WaitForSeconds(10f);
        yield return parentShip.MoveRelative(Vector2.Reflect(moveDirection, Vector2.up), 3f, 2.5f);

        yield return LeaveScene();
    }
}