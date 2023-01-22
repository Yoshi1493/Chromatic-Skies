using System.Collections;

public class AquariusMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f, delay: 3f);
    }
}