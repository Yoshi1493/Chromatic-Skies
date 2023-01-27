using System.Collections;

public class LibraBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}