using System.Collections;

public class GeminiMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2.5f, 1f, 3f);
    }
}