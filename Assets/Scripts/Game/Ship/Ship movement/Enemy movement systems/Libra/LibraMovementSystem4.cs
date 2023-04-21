using System.Collections;

public class LibraMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return this.MoveToRandomPosition(0.5f);
        }
    }
}