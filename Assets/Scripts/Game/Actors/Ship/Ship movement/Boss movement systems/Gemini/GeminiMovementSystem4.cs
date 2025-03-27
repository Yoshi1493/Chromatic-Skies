using System.Collections;
using static CoroutineHelper;

public class GeminiMovementSystem4 : BossMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2f);
            yield return this.MoveToRandomPosition(2f);
        }
    }
}