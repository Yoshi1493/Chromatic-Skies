using System.Collections;

public class PiscesMovementSystem6 : EnemyMovement
{
    public const float MoveDuration = 1.2f;

    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(MoveDuration, 3f, 4f);
    }
}