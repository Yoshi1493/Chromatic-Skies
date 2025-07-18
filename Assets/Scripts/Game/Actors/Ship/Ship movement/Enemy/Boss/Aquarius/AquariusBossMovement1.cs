using System.Collections;

public class AquariusBossMovement1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(2f);
    }
}