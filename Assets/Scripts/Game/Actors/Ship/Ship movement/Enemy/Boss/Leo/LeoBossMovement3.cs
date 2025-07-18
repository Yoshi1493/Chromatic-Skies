using System.Collections;
using static CoroutineHelper;

public class LeoBossMovement3 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(5f);

        while (enabled)
        {
            yield return WaitForSeconds(5f);
            yield return parentShip.MoveToRandomPosition(1f, 2f, 3f);
        }
    }
}