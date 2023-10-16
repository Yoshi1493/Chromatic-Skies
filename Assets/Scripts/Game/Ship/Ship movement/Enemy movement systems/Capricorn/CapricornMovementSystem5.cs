using System.Collections;

public class CapricornMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, 3f, 4f);
    }
}