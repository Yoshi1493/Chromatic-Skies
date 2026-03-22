using System.Collections;

public class LeoBullet06 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 5f, 1f);
    }
}