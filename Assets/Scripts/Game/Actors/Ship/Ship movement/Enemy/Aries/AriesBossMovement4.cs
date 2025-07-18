using System.Collections;
using static CoroutineHelper;

public class AriesBossMovement4 : BossMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(3f);
            yield return parentShip.MoveToRandomPosition(1f, 2f, 2f);
        }
    }
}