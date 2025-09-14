using System.Collections;
using UnityEngine;

public class VirgoCommonMovement2 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float r = Random.Range(-30f, 30f);

        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(r), 1.5f, 3f);
        yield return null;
        yield return parentShip.MoveRelativeLinear(Vector3.down.RotateVectorBy(-r), 2f, 5f);

        yield return LeaveScene();
    }
}