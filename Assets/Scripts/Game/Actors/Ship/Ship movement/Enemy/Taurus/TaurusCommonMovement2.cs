using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class TaurusCommonMovement2 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        int d = PositiveOrNegativeOne;
        float s = 5f;
        float r = Random.Range(0.8f, 1.0f);

        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(15f * d), s, r);
        yield return WaitForSeconds(4f);
        yield return parentShip.MoveRelative(Vector3.up.RotateVectorBy(15f * d), s / 2f, r * 2f);

        yield return LeaveScene();
    }
}