using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiCommonMovement6 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        parentShip.transform.position += Random.Range(-5f, 5f) * Vector3.right;
        yield return parentShip.MoveTo(ActorMovementHelper.bossSpawnPosition, 1.5f);

        while (enabled)
        {
            yield return WaitForSeconds(8f);
            yield return parentShip.MoveToRandomPosition(1f, 2f, 3f);
            yield return parentShip.MoveToRandomPosition(1f, 2f, 3f);
        }
    }
}