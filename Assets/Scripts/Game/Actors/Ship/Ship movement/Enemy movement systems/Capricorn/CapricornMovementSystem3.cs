using System.Collections;

public class CapricornMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return this.MoveToRandomPosition(0.5f, 1.5f, 3f);
        }
    }
}