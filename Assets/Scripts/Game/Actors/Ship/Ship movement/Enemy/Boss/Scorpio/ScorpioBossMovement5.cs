using System.Collections;
using static CoroutineHelper;

public class ScorpioBossMovement5 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(2f, 3f, 5f);

        yield return WaitForSeconds(2.5f);
        yield return parentShip.MoveToRandomPosition(1f, 2f, 4f);
    }
}