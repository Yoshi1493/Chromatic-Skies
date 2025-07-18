using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusCommonMovement05 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveTo(ActorMovementHelper.bossSpawnPosition, 2f);

        while (enabled)
        {
            yield return WaitForSeconds(3f);
            yield return parentShip.MoveToRandomPosition(1f, delay: 3f);
        }
    }
}