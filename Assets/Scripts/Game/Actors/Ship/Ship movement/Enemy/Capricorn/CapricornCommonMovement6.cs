using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornCommonMovement6 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveTo(ActorMovementHelper.bossSpawnPosition, 1.5f);

        while (enabled)
        {
            yield return WaitForSeconds(5f);
            yield return parentShip.MoveToRandomPosition(2f, 3f, 4f);
        }
    }
}