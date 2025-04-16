using System.Collections;

public class LibraMovementSystem5 : BossMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return parentShip.MoveToRandomPosition(1f);
        }
    }
}