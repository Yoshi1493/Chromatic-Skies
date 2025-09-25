using System.Collections;
using UnityEngine;

public class VirgoCommonMovement8 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(Random.Range(-10f, 10f)), 2f, 6f);

        yield return LeaveScene();
    }
}