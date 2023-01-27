using System.Collections;
using static CoroutineHelper;

public class LibraMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(5f);
            yield return this.MoveToRandomPosition(1f);
        }
    }
}