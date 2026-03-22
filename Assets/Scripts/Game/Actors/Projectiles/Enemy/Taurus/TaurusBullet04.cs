using System.Collections;

public class TaurusBullet04 : MinibossBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 1f, 0.5f);
        yield return this.LerpSpeed(1f, 1.5f, 1.5f);
    }
}