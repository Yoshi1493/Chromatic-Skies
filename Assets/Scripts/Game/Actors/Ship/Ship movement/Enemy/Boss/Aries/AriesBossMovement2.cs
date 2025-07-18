using System.Collections;

public class AriesBossMovement2 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f);
    }
}