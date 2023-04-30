using System.Collections;

public class SagittariusMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(0.8f, 1f, 2f);
    }
}