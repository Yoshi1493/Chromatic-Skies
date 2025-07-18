using System.Collections;

public class GeminiBossMovement1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(2.5f, 1f, 3f);
    }
}