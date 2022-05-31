using System.Collections;

public class TaurusBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(7f, 2f, 1f);
    }
}