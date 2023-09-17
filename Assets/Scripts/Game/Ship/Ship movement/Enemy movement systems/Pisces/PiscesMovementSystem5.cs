using System.Collections;

public class PiscesMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, 1f, 2f);
    }
}