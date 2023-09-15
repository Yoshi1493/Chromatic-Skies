using System.Collections;
using static CoroutineHelper;

public class GeminiMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f, 4f, 4f);

        while (enabled)
        {
            //for (int i = 0; i < 10; i++)
            //{
                yield return WaitForSeconds(1f);
                yield return this.MoveToRandomPosition(1f, 0.5f, 1f);
            //}
        }
    }
}