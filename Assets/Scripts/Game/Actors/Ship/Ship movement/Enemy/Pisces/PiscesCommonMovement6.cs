using System.Collections;

public class PiscesCommonMovement6 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveTo(ActorMovementHelper.bossSpawnPosition, 1.5f);
    }
}