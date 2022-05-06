using System.Collections;

public class LeoBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}