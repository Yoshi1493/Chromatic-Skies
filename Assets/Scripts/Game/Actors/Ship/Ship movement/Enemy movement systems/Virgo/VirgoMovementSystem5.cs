using System.Collections;
using static CoroutineHelper;

public class VirgoMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(6f);
            yield return this.MoveToRandomPosition(1.2f, 2f, 2f);
        }
    }
}