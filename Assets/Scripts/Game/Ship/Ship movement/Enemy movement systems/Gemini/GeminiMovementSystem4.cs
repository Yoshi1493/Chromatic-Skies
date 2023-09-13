using System.Collections;

public class GeminiMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f, 4f, 9f);
    }
}