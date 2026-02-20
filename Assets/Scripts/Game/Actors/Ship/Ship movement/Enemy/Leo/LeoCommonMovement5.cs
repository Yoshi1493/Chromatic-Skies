using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class LeoCommonMovement5 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = new (ScreenHalfWidth * Random.Range(-0.75f, 0.75f), ScreenHalfHeight * 1.1f);

        parentShip.transform.position = p0;
        float d = -SignX;

        yield return parentShip.SpiralIntoPoint(ActorMovementHelper.bossSpawnPosition, d * 45f, 1f);

        while (enabled)
        {
            yield break;
        }    
    }
}