using System.Collections;

public class GeminiBullet52 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return null;
    }
}