using System.Collections;

public class GeminiBullet32 : BossBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2f, 1f);
    }
}