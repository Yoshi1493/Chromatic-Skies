using System.Collections;

public class CancerBullet01 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, -4f, 0.5f);
        yield return this.LerpSpeed(-4f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, 4f, 1f);
    }
}