using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class PiscesCommonMovement3 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 v = Vector3.down.RotateVectorBy(PositiveOrNegativeOne * Random.Range(5f, 15f));

        yield return parentShip.MoveRelative(v, 2f, 2f);
        yield return WaitForSeconds(8f);
        yield return parentShip.MoveRelative(Vector2.Reflect(v, Vector2.up), 2f, 2f);

        yield return LeaveScene();
    }
}