using System.Collections;

public class LeoBullet31 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0.2f, 0.5f);
        yield return this.LerpSpeed(0.2f, 2f, 1f);
    }
}