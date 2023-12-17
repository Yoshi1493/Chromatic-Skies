using System.Collections;
using static CoroutineHelper;

public class GeminiMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return this.MoveToRandomPosition(0.5f, 1f, 2f);
            yield return WaitForSeconds(1f);
        }
    }
}