using System.Collections;

public class LeoBullet60 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2f, 1f);
    }
}