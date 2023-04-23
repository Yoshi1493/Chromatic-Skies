using System.Collections;

public class LeoBullet32 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield break;
    }
}