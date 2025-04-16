using System.Collections;
using static CoroutineHelper;

public class ScorpioMovementSystem3 : BossMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2.5f);
            yield return parentShip.MoveToRandomPosition(1.5f);
        }
    }
}