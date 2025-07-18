using System.Collections;

public class LibraBossMovement3 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f);
    }
}