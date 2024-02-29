using System.Collections;

public class LeoBullet30 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0.5f, 0.5f);
        yield return this.LerpSpeed(0.5f, 2.5f, 3f);
    }
}