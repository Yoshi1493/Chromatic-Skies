using System.Collections;
using static CoroutineHelper;

public class SagittariusBossMovement2 : BossMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(3f);
            yield return parentShip.MoveToRandomPosition(2f, 3f, 4f);
        }
    }
}