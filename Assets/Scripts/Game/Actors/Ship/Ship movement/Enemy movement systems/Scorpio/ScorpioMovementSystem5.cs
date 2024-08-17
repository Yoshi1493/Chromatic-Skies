using System.Collections;
using static CoroutineHelper;

public class ScorpioMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(3f);
        yield return this.MoveToRandomPosition(1.5f, 3f, 5f);
    }
}