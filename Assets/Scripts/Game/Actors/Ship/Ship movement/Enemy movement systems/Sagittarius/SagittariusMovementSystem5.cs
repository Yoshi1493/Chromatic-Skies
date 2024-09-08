using System.Collections;
using static CoroutineHelper;

public class SagittariusMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            yield return WaitForSeconds(4f);
            yield return this.MoveToRandomPosition(1f, 2f, 3f);
        }
    }
}