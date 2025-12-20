using System.Collections;
using UnityEngine;

public class GeminiCommonMovement7 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        parentShip.transform.position += Random.Range(-1f, 1f) * Vector3.up;

        float d = SignX;
        yield return parentShip.MoveRelative(d * Vector3.left.RotateVectorBy(d * 15f), 4f, 5.5f);

        yield return LeaveScene();
    }
}