using System.Collections;

public class PiscesMovementSystem2 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(2f, 3f, 5f);
    }
}