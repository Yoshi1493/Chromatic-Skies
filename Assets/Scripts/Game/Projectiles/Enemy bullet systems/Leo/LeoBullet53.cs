using System.Collections;

public class LeoBullet53 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 3f, 0.5f);
    }
}