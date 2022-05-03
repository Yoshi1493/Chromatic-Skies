using System.Collections;

public class LeoBullet23 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 2f, 1f);
    }
}