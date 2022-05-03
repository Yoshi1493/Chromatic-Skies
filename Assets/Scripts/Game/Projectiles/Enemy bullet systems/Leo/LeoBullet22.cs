using System.Collections;

public class LeoBullet22 : EnemyBullet
{
    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 1f, 0.25f);
        yield return this.LerpSpeed(1f, -1f, 0.5f);
        yield return this.LerpSpeed(-1f, 0f, 0.25f);
    }
}