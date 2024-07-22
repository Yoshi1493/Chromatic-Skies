using System.Collections;

public class TaurusBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 4f, 3f);
    }
}