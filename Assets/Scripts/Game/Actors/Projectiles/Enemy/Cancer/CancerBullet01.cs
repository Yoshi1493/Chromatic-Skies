using System.Collections;

public class CancerBullet01 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, -4f, 0.5f);
        yield return this.LerpSpeed(-4f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}