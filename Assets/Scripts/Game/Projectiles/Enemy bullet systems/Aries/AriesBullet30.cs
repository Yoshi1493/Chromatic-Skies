using System.Collections;

public class AriesBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0.5f, 0.5f);
        yield return this.LerpSpeed(0.5f, 3f, 0.5f);
    }
}