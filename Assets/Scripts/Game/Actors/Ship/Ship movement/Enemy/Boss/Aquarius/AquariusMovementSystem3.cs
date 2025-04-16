using System.Collections;

public class AquariusMovementSystem3 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(2f);
    }
}