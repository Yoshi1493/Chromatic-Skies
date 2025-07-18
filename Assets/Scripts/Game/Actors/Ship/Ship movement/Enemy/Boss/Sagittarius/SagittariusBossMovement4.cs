using System.Collections;

public class SagittariusBossMovement4 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(1f, 1.5f, 3f);
    }
}