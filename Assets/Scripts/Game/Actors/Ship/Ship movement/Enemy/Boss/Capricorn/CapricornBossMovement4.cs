using System.Collections;

public class CapricornBossMovement4 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f, delay: 5f);
    }
}