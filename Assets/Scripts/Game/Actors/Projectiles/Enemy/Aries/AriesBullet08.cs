using System.Collections;

public class AriesBullet08 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2.5f, 1f);
    }
}