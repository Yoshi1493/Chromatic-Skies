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
            for (int i = 0; i < 16; i++)
            {
                yield return WaitForSeconds(0.1f);
            }

            yield return WaitForSeconds(3f);
        }
    }
}