using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornCommonMovement3 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        parentShip.transform.position += Random.Range(-1f, 1f) * Vector3.up;
        yield return parentShip.MoveRelative((SignX * Vector3.left).RotateVectorBy(Random.Range(-5f, 5f)), Random.Range(3f, 6f), 2f);
        yield return WaitForSeconds(1f);

        yield return LeaveScene();
    }
}