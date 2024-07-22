using System.Collections;

public class LeoMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, 3f, 4f);
    }
}