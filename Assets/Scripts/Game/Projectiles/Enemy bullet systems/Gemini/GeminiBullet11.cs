using System.Collections;

public class GeminiBullet11 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;
        yield return null;
    }
}