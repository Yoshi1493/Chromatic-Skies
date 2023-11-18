using System.Collections;
using static CoroutineHelper;

public class VirgoMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < 2; i++)
        {
            yield return this.MoveToRandomPosition(1.5f, 1.5f, 3f);
        }
    }
}