using System.Collections;

public class LibraBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 7f;
        yield return null;
    }
}