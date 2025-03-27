using System.Collections;

public class CancerMovementSystem1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1.5f);
        yield return this.MoveToRandomPosition(1.5f);
    }
}