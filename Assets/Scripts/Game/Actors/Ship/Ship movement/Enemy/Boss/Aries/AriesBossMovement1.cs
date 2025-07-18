using System.Collections;

public class AriesBossMovement1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f);
    }
}