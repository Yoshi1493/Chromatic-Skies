using System.Collections;

public class LeoBossMovement2 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(2f);
    }
}