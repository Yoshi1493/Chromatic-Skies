using System.Collections;

public class GeminiBullet52 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 2.5f, 2f);
    }
}