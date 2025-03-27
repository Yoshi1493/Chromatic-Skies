using System.Collections;

public class GeminiMovementSystem2 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, 0.5f, 1f);
    }
}