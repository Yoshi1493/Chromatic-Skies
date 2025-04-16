using System.Collections;

public class CapricornMovementSystem5 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f, 3f, 4f);
    }
}