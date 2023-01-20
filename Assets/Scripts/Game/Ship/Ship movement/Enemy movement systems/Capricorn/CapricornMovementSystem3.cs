using System.Collections;

public class CapricornMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f);
    }

    protected override void OnAttackFinish()
    {
        StartMove();
    }
}