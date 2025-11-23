using System.Collections;

public class TaurusBullet05 : MinibossBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 2f, 1f);
    }
}