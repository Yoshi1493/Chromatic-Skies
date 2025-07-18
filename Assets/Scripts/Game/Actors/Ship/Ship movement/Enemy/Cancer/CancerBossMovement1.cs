using System.Collections;

public class CancerBossMovement1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1.5f);
        yield return parentShip.MoveToRandomPosition(1.5f);
    }
}