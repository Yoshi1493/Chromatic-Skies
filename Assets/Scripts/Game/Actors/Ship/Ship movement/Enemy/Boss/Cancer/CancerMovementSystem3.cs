using System.Collections;

public class CancerMovementSystem3 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f, 3f, 4f);
    }
}