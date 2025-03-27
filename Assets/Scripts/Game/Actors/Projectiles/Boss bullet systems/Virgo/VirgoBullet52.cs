using System.Collections;

public class VirgoBullet52 : BossBullet
{
    protected override float MaxLifetime => 9f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}