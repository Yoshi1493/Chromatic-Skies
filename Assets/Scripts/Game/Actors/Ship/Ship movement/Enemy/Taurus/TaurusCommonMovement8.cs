using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusCommonMovement8 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, Random.Range(3f, 4.5f), 1f);
        yield return WaitForSeconds(5f);

        yield return LeaveScene();
    }
}