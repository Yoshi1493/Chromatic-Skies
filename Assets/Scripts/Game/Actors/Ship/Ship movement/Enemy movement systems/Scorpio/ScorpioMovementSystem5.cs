using System.Collections;
using static CoroutineHelper;

public class ScorpioMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f, 3f, 5f);

        yield return WaitForSeconds(2.5f);
        yield return this.MoveToRandomPosition(1f, 2f, 4f);
    }
}