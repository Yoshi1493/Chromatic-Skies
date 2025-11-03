using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerCommonMovement5 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.SpiralIntoPoint(ActorMovementHelper.bossSpawnPosition, 60f, 1.5f);

        while (enabled)
        {
            yield return WaitForSeconds(6f);

            Vector3 pos = parentShip.transform.position.GetRandomPositionWithinBounds(parentShip.shipData.boundaryLayer, 3f, 5f);
            yield return parentShip.SpiralIntoPoint(pos, 90f * Mathf.Sign(pos.x - parentShip.transform.position.x), 1.5f);
        }
    }
}