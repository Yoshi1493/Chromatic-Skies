using System.Collections;
using static CoroutineHelper;

public class PiscesMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2f);
            yield return this.MoveToRandomPosition(1f, 1f, 2f);
        }
    }
}