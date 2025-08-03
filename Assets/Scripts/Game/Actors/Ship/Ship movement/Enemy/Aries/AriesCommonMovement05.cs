using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesCommonMovement05 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveTo(ActorMovementHelper.bossSpawnPosition, 1.5f);

        while (enabled && parentShip.HealthPercent < 0.5f)
        {
            yield return WaitForSeconds(4f);
            yield return parentShip.TranslateAround(parentShip.transform.position + Vector3.down, 360f, 1.5f);
            yield return WaitForSeconds(2.5f);
        }
    }
}