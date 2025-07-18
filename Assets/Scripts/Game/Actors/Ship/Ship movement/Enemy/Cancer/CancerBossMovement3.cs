using System.Collections;

public class CancerBossMovement3 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(2f, 3f, 4f);
    }
}