using System.Collections;

public class PiscesMovementSystem6 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1.5f, 3f, 4f);
    }
}