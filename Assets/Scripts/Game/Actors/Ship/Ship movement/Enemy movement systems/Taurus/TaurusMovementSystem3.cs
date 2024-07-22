using System.Collections;

public class TaurusMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f, 3f, 5f);
    }
}