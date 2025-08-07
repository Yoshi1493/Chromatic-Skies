using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 pos = parentShip.transform.position;
        float d = -Mathf.Sign(pos.x);

        yield return parentShip.MoveRelativeLinear(Vector3.down, Random.Range(2.5f, 3f), 1f);
        yield return WaitForSeconds(1f);
        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(d * 45f), 4f, 3f);
    }
}