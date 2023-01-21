using System.Collections;

public class AriesMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return this.MoveToRandomPosition(1f, 1f, 2f, 2f);
        }
    }

    protected override void OnAttackStart(int _)
    {
        StartMove();
    }
}