using System.Collections;

public class LeoBullet10 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 5f, 1f);
    }
}