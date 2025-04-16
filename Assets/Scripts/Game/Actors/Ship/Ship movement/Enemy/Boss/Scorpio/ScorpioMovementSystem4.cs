using System.Collections;

public class ScorpioMovementSystem4 : BossMovement
{
    protected override IEnumerator Move()
    {
        for (int i = 0; i < 4; i++)
        {
            float d = i + 1;
            yield return parentShip.MoveToRandomPosition(0.8f, d, d);
        }
    }
}