using System.Collections;

public class CapricornMovementSystem4 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, delay: 5f);
    }
}