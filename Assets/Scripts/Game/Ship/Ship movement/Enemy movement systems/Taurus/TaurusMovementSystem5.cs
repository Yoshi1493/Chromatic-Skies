using System.Collections;
using static CoroutineHelper;

public class TaurusMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
        yield return this.MoveToRandomPosition(1f, 3f, 5f);
    }
}