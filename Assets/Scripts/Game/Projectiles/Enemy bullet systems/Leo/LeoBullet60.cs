using System.Collections;

public class LeoBullet60 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(-3f, 3f, 1f);
    }
}