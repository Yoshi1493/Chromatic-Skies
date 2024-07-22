using System.Collections;

public class LibraMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return this.MoveToRandomPosition(1f);
        }
    }
}