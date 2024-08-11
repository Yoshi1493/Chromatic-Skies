using System.Collections;

public class LibraBullet69 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield break;
    }
}