using System.Collections;

public class LeoBullet32 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0.5f, 0.5f);
        yield return this.LerpSpeed(0.5f, 1.5f, 1f);
    }
}