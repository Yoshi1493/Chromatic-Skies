using System.Collections;

public class ScorpioBullet10 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield break;
    }
}