using System.Collections;

public class GeminiBullet21 : BossBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 2f, 1f);
    }
}