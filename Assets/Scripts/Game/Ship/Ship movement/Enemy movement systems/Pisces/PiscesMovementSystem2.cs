using System.Collections;

public class PiscesMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f);
    }
}