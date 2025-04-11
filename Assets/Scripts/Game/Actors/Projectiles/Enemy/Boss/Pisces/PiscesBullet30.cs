using System.Collections;

public class PiscesBullet30 : BossBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}