using System.Collections;

public class LeoBullet53 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 2.5f, 0.5f);
    }
}