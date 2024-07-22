using System.Collections;

public class CancerBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield break;
    }
}