using System.Collections;

public class CapricornBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(8f, 2f, 0.5f);
        yield return this.LerpSpeed(2f, 0f, 1f);
        yield return this.LerpSpeed(0f, -3f, 1f);
    }
}