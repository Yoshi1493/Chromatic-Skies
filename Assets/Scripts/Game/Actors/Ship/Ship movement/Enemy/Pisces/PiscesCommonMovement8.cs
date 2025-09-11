using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesCommonMovement8 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 v = Vector3.down.RotateVectorBy(-SignX * Random.Range(30f, 45f));

        yield return parentShip.MoveRelative(v, 3f, 1.5f);
        yield return WaitForSeconds(5f);
        yield return parentShip.MoveRelativeLinear(Vector2.Reflect(v, Vector2.up), 3f, 1.5f);

        yield return LeaveScene();
    }
}