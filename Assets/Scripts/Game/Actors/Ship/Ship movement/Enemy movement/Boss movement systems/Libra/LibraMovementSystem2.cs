using System.Collections;

public class LibraMovementSystem2 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f);
    }
}