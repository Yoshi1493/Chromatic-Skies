using System.Collections;

public class VirgoMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f);
    }
}