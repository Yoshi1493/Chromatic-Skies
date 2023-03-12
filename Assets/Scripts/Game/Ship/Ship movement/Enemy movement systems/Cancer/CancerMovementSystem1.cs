using System.Collections;

public class CancerMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f);
    }
}