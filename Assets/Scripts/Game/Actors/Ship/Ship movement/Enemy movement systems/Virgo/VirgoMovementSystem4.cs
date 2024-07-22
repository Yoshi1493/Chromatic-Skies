using System.Collections;
using static CoroutineHelper;

public class VirgoMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return WaitForSeconds(2f);
            yield return this.MoveToRandomPosition(2f);
        }
    }
}
