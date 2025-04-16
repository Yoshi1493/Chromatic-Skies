using System.Collections;

public class TaurusMovementSystem2 : BossMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return parentShip.MoveToRandomPosition(0.6f, 3f, 3f);
        }
    }
}