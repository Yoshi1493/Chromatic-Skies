using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class PiscesCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float r = Random.Range(1f, 1.5f);
        float d = PositiveOrNegativeOne;

        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(30f * d), 3f, r);
        yield return WaitForSeconds(2f);
        yield return parentShip.MoveRelative(Vector3.up.RotateVectorBy(30f * -d), 2f, r * 1.5f);

        yield return LeaveScene();
    }
}