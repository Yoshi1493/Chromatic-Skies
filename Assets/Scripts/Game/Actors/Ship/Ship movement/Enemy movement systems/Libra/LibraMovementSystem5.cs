using System.Collections;

public class LibraMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f);
    }
}