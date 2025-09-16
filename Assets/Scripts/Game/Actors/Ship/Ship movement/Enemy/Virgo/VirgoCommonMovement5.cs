using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement5 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveTo(ActorMovementHelper.bossSpawnPosition, 1.5f);
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            yield return parentShip.MoveToRandomPosition(1f);
            yield return parentShip.MoveToRandomPosition(1f);
            yield return parentShip.MoveToRandomPosition(1f);

            yield return WaitForSeconds(5f);
        }
    }
}