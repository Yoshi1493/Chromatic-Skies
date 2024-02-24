using System.Collections;
using static CoroutineHelper;

public class LeoMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(0.2f);
        yield return this.MoveToRandomPosition(1f, 3f, 5f);
    }
}