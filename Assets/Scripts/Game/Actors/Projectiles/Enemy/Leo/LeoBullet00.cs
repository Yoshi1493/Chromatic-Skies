using System.Collections;

public class LeoBullet00 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 1.5f, 1.5f);
    }
}