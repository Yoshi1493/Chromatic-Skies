using System.Collections;
using static CoroutineHelper;

public class TaurusMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            yield return WaitForSeconds(2f);
            yield return this.MoveToRandomPosition(1f, 1f, 4f);
        }
    }
}