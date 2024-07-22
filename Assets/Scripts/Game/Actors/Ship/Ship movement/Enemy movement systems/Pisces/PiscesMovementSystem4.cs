using System.Collections;

public class PiscesMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f);
    }
}