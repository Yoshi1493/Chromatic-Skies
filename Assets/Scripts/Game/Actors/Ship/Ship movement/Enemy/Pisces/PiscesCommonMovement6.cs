using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesCommonMovement6 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveTo(ActorMovementHelper.bossSpawnPosition, 1.5f);

    }
}