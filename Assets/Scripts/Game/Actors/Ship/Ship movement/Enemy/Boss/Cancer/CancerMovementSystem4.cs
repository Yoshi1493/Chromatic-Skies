using System.Collections;

public class CancerMovementSystem4 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f, 3f, 5f);
    }
}