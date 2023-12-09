using System.Collections;

public class TaurusBullet50 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2.5f, 3f);
    }
}