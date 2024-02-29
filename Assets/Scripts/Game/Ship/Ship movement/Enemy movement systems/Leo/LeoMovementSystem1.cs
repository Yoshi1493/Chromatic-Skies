using System.Collections;
using static CoroutineHelper;

public class LeoMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
        yield return this.MoveToRandomPosition(4f);
    }
}