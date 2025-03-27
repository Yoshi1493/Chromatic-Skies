using System.Collections;

public class GeminiBullet42 : BossBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3.5f, 2f, 2f);
    }
}