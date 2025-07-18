using System.Collections;

public class GeminiBossMovement2 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f, 0.5f, 1f);
    }
}