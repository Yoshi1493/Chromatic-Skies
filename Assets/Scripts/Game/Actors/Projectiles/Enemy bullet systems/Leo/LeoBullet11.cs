using System.Collections;

public class LeoBullet11 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0.5f, 1f);
    }
}