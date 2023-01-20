using System.Collections;

public class CapricornMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f, 4f, 4f);
    }

    protected override void OnAttackFinish()
    {
        StartMove();
    }
}