using System.Collections;
using static CoroutineHelper;

public class LibraMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(4f);
            yield return this.MoveToRandomPosition(1f);
        }
    }
}