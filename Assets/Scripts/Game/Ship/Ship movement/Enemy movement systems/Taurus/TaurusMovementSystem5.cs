using System.Collections;
using static CoroutineHelper;

public class TaurusMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(3f);
            yield return this.MoveToRandomPosition(1f);
        }
    }
}