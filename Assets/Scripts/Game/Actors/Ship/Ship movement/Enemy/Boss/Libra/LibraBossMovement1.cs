using System.Collections;
using static CoroutineHelper;

public class LibraBossMovement1 : BossMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(5f);
            yield return parentShip.MoveToRandomPosition(2f);
        }
    }
}