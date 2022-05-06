using System.Collections;

public class LeoBullet30 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}