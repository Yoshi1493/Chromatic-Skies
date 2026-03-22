using System.Collections;
using static CoroutineHelper;

public class VirgoCommonMovement5 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.SpiralIntoPoint(ActorMovementHelper.bossSpawnPosition, 120f, 1.5f);

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