using System.Collections;
using static CoroutineHelper;

public class VirgoCommonMovement6 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.SpiralIntoPoint(new(-4f, 2f), -180f, 1.5f);
        yield return WaitForSeconds(6f);

        yield return LeaveScene();
    }
}