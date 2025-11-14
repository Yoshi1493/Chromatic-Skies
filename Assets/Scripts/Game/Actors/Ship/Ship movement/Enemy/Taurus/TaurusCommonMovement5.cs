using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusCommonMovement5 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = ActorMovementHelper.bossSpawnPosition;
        Vector3 p1 = Vector3.LerpUnclamped(parentShip.transform.position, p0, 1.1f);

        yield return parentShip.MoveTo(p1, 1.2f);
        yield return parentShip.MoveTo(p0, 0.5f);

        while (enabled)
        {
            yield return WaitForSeconds(10f);
        }
    }
}