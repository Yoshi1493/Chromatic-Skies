using System.Collections;

public class GeminiMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, 1f, 2f);
    }
}