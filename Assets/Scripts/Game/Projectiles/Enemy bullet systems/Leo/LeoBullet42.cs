using System.Collections;

public class LeoBullet42 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}