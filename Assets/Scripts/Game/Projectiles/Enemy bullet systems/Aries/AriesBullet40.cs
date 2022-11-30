using System.Collections;

public class AriesBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 1f, 0.5f);
        yield return this.LerpSpeed(1f, 2.5f, 1f);
    }
}