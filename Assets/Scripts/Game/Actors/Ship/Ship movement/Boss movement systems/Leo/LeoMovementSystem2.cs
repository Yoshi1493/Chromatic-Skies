using System.Collections;

public class LeoMovementSystem2 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f);
    }
}