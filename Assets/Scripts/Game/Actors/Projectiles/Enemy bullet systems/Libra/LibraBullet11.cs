using System.Collections;

public class LibraBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield break;
    }
}