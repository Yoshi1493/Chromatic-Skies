using System.Collections;
using static CoroutineHelper;

public class VirgoBossMovement4 : BossMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return WaitForSeconds(2f);
            yield return parentShip.MoveToRandomPosition(2f);
        }
    }
}
