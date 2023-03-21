using System.Collections;
using static CoroutineHelper;

public class CancerMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            yield return WaitForSeconds(7f);
            yield return this.MoveToRandomPosition(1f);
        }
    }
}