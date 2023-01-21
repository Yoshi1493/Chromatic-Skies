using System.Collections;

public class TaurusMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f, 1f, 4f);
    }

    protected override void OnAttackFinish()
    {
        StartMove();
    }
}