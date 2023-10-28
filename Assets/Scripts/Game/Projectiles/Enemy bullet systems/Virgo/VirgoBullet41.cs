using System.Collections;

public class VirgoBullet41 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}