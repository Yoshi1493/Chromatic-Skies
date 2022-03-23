using System.Collections;

public class LibraBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;
        yield return null;
    }
}