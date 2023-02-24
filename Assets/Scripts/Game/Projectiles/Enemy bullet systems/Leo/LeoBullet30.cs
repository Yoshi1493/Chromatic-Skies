using System.Collections;

public class LeoBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 0.5f, 0.5f);
        yield return this.LerpSpeed(0.5f, 2.5f, 1f);
    }
}