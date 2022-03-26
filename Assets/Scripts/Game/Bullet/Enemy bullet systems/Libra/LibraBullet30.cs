using System.Collections;

public class LibraBullet30 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return null;
    }
}