using System.Collections;

public class TaurusBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 1f, 0.5f);
        yield return this.LerpSpeed(1f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, 2.5f, 2f);
    }
}