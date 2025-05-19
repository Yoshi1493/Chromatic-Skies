using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusMovementSystem05 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveTo(ActorMovementHelper.bossSpawnPosition, 1f);

        while (enabled)
        {
            yield return WaitForSeconds(3f);
            yield return parentShip.MoveToRandomPosition(1f, delay: 3f);
        }
    }
}