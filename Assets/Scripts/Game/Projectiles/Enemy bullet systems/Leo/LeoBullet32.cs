using System.Collections;

public class LeoBullet32 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 1.5f, 1f);
    }
}