using System.Collections;

public class LeoBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 2f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return null;
    }
}