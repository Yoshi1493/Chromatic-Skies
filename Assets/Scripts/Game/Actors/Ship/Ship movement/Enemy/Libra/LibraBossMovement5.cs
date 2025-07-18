using System.Collections;

public class LibraBossMovement5 : BossMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return parentShip.MoveToRandomPosition(1f);
        }
    }
}