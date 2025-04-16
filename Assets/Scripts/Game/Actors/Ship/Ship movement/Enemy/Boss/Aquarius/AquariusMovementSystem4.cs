using System.Collections;
using static CoroutineHelper;

public class AquariusMovementSystem4 : BossMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(4f);
            yield return parentShip.MoveToRandomPosition(1f);
        }
    }
}