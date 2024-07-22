using System.Collections;

public class SagittariusMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, 1.5f, 3f);
    }
}