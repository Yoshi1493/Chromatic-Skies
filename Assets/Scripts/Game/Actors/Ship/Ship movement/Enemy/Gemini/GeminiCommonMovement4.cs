using System.Collections;
using UnityEngine;

public class GeminiCommonMovement4 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = SignX;
        yield return parentShip.MoveRelative(d * Vector3.left.RotateVectorBy(Random.Range(-10f, 10f)), 3f, 7f);

        yield return LeaveScene();
    }
}