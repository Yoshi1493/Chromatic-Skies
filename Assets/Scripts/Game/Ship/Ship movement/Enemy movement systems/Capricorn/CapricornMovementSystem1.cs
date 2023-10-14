using System.Collections;
using static CoroutineHelper;

public class CapricornMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
        yield return this.MoveToRandomPosition(1f, 1f, 3f);
        yield return WaitForSeconds(1f);
        yield return this.MoveToRandomPosition(1f, 1f, 3f);
        yield return this.MoveToRandomPosition(2f, 3f, 5f);
    }
}