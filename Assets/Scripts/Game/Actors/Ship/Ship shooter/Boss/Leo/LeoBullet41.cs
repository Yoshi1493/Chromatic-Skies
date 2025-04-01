using System.Collections;

public class LeoBullet41 : BossBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}