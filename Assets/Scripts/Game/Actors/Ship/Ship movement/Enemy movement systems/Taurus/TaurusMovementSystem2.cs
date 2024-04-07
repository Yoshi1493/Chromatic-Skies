using System.Collections;

public class TaurusMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return this.MoveToRandomPosition(0.6f, 3f, 3f);
        }
    }
}