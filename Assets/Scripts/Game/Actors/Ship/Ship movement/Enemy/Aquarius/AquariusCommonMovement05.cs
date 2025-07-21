using System.Collections;
using static CoroutineHelper;

public class AquariusCommonMovement05 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveTo(ActorMovementHelper.bossSpawnPosition, 1.5f);
        yield return WaitForSeconds(0.5f);

        while (enabled)
        {
            yield return WaitForSeconds(3f);
            yield return parentShip.MoveToRandomPosition(1f, delay: 3f);
        }
    }
}