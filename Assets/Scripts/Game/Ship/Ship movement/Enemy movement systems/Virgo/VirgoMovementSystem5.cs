using System.Collections;
using static CoroutineHelper;

public class VirgoMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(1f);
            yield return this.MoveToRandomPosition(1f, 1f, 2f);
        }
    }
}