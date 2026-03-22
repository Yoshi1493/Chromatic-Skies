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
            yield return WaitForSeconds(6.6f);

            for (int i = 0; i < 3; i++)
            {
                yield return parentShip.MoveToRandomPosition(1f);
            }

            yield return WaitForSeconds(2f);
        }
    }
}