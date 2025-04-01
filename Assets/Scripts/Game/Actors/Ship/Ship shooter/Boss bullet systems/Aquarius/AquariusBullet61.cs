using System.Collections;

public class AquariusBullet61 : BossBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;
        yield return null;
    }
}