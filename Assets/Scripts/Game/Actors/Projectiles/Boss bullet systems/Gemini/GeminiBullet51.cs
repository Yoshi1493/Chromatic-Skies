using System.Collections;

public class GeminiBullet51 : BossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
        yield return this.LerpSpeed(3f, 2f, 2f);
    }
}