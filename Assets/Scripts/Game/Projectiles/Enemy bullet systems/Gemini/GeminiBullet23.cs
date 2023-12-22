using System.Collections;

public class GeminiBullet23 : EnemyBullet
{
    protected override float MaxLifetime => 9f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2f, 0.5f);
    }
}