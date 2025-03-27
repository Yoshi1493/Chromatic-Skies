using System.Collections;

public class CancerBullet11 : BossBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield break;
    }
}