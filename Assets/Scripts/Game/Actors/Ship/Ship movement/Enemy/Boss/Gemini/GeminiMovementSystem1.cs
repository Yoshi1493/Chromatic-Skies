using System.Collections;

public class GeminiMovementSystem1 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2.5f, 1f, 3f);
    }
}