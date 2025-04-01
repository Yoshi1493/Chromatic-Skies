using System.Collections;

public class TaurusMovementSystem1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f);
    }
}