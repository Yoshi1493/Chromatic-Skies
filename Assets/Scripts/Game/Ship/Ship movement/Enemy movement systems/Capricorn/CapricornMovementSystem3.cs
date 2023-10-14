using System.Collections;

public class CapricornMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1.5f, 3f, 5f);
    }
}