using System.Collections;

public class CancerBullet03 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}