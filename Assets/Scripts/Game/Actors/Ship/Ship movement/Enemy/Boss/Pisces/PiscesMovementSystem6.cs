using System.Collections;

public class PiscesMovementSystem6 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1.5f, 3f, 4f);
    }
}