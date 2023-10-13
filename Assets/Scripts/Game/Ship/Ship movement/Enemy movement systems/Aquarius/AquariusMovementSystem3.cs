using System.Collections;

public class AquariusMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(0.5f, 1f, 2f);
        yield return this.MoveToRandomPosition(0.5f, 1f, 2f);
        yield return this.MoveToRandomPosition(1f);
    }
}