using System.Collections;

public class VirgoMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return this.MoveToRandomPosition(1f, delay: 5f);
        }
    }
}
