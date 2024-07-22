using System.Collections;
using static CoroutineHelper;

public class LeoMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(5f);

        while (enabled)
        {
            yield return WaitForSeconds(5f);
            yield return this.MoveToRandomPosition(1f, 2f, 3f);
        }
    }
}