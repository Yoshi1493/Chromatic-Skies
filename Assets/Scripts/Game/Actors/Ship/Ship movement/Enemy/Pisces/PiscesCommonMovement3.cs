using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class PiscesCommonMovement3 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        int d = PositiveOrNegativeOne;
        Vector3 v = Vector3.down.RotateVectorBy(Random.Range(30f, 45f));
        v.x *= d;

        yield return parentShip.MoveRelative(v, 3f, 1.5f);
        yield return WaitForSeconds(3f);
        yield return parentShip.MoveRelativeLinear(Vector2.Reflect(v, Vector2.up), 3f, 1.5f);

        yield return LeaveScene();
    }
}