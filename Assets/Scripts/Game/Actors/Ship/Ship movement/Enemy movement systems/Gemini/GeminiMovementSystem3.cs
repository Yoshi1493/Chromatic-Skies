using System.Collections;
using static CoroutineHelper;

public class GeminiMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return WaitForSeconds(1f);
            yield return this.MoveToRandomPosition(1f, 1f, 2f);
        }
    }
}