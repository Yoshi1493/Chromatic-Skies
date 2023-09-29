using System.Collections;

public class TaurusMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(0.5f);
    }
}