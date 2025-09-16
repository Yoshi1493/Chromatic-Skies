using System.Collections;

public class VirgoBullet06 : MinibossBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 4f, 2f);
    }
}