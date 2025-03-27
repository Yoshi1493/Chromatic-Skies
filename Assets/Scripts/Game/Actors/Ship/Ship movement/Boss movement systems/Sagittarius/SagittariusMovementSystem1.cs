using System.Collections;

public class SagittariusMovementSystem1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, 1f, 2f);
    }
}