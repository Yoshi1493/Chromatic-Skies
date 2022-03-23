using System.Collections;

public class LibraBullet10 : EnemyBullet
{
    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 5f;
        yield return null;
    }
}