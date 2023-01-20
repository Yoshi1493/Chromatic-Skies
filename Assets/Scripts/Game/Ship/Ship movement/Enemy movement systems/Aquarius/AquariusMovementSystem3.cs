using System.Collections;

public class AquariusMovementSystem3 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(2f, delay: 8f);
    }

    protected override void OnAttackFinish()
    {
        StartMove();
    }
}