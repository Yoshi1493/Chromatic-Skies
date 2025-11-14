using System.Collections;
using UnityEngine;

public class TaurusCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = SignX;

        parentShip.transform.position += d * Random.Range(0f, 2f) * Vector3.right;
        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(-d * 15f), 4f, 3f);

        yield return LeaveScene();
    }
}