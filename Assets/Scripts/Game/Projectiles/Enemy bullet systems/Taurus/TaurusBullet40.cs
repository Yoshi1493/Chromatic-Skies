using System.Collections;

public class TaurusBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 0.5f);
        yield return this.RotateBy(180f, 0f);
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}