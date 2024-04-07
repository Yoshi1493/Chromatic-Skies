using System.Collections;

public class AquariusBullet61 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;
        yield return null;
    }
}