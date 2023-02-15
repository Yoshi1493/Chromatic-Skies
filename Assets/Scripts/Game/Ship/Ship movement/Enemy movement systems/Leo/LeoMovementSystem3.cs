using System.Collections;
using static CoroutineHelper;

public class LeoMovementSystem3 : EnemyMovement
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