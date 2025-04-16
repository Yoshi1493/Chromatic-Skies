using System.Collections;

public class SagittariusMovementSystem6 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f, 3f, 4f);
    }
}