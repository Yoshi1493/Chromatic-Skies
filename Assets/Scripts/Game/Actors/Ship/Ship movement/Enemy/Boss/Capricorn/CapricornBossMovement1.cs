using System.Collections;
using static CoroutineHelper;

public class CapricornBossMovement1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
        yield return parentShip.MoveToRandomPosition(1f, 1f, 2f);
        yield return WaitForSeconds(2f);
        yield return parentShip.MoveToRandomPosition(2f);
    }
}