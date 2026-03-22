using System.Collections;

public class TaurusBullet03 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 3.5f, 2f);
    }
}