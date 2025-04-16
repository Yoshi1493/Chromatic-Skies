using System.Collections;

public class SagittariusMovementSystem1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f, 1f, 2f);
    }
}