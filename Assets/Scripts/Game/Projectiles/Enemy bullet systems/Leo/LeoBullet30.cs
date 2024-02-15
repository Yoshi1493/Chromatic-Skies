using System.Collections;

public class LeoBullet30 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 2f, 1f);
    }
}