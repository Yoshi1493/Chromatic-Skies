using System.Collections;
using static CoroutineHelper;

public class SagittariusBossMovement5 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            yield return WaitForSeconds(4f);
            yield return parentShip.MoveToRandomPosition(1f, 2f, 3f);
        }
    }
}