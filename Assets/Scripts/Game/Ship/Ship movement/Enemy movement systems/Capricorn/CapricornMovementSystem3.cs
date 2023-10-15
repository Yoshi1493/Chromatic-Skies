using System.Collections;

public class CapricornMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, 1f, 2f);
        yield return this.MoveToRandomPosition(1f, 2f, 3f);
        yield return this.MoveToRandomPosition(1f, 3f, 4f);
    }
}