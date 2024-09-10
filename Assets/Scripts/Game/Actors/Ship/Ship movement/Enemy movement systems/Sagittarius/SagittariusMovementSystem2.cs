using System.Collections;
using static CoroutineHelper;

public class SagittariusMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(3f);
            yield return this.MoveToRandomPosition(2f, 3f, 4f);
        }
    }
}