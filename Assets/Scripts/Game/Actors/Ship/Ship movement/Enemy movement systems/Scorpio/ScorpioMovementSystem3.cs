using System.Collections;
using static CoroutineHelper;

public class ScorpioMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2.5f);
            yield return this.MoveToRandomPosition(1.5f);
        }
    }
}