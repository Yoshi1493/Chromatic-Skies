using System.Collections;

public class CancerBossMovement4 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f, 3f, 5f);
    }
}