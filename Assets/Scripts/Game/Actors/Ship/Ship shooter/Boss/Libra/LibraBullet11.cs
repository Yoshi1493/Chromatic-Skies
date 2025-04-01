using System.Collections;

public class LibraBullet11 : BossBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield break;
    }
}