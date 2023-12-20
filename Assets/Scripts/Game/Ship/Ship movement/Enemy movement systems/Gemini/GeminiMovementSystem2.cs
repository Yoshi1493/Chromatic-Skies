using System.Collections;
using static CoroutineHelper;

public class GeminiMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            for (int ii = 0; ii < 3; ii++)
            {
                yield return this.MoveToRandomPosition(1f, 1f, 2f);
            }

            yield return WaitForSeconds(5f);
        }
    }
}