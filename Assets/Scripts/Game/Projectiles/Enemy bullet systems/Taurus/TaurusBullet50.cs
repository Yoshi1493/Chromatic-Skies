using System.Collections;

public class TaurusBullet50 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2f, 3f);
    }
}