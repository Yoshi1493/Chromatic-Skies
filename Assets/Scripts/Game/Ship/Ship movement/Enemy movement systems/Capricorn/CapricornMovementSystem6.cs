using System.Collections;

public class CapricornMovementSystem6 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f, 4f, 4f);
    }
}