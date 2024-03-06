using System.Collections;

public class LeoBullet54 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0.5f, 1f);
        yield return this.LerpSpeed(0.5f, 2.5f, 1f);
    }
}