using System.Collections;
using static CoroutineHelper;

public class SagittariusMovementSystem2 : EnemyMovement
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