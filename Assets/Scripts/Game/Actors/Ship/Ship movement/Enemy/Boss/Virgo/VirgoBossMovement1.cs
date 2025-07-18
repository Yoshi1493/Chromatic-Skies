using System.Collections;
using static CoroutineHelper;

public class VirgoBossMovement1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            yield return WaitForSeconds(2f);
            yield return parentShip.MoveToRandomPosition(1f, 1f, 2f);
        }
    }
}