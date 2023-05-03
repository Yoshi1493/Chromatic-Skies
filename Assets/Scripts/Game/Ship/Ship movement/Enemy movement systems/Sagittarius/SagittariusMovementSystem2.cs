using System.Collections;
using static CoroutineHelper;

public class SagittariusMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        //yield return WaitForSeconds(3f);

        while (enabled)
        {
            yield return WaitForSeconds(5f);
            yield return this.MoveToRandomPosition(1f);
        }
    }
}