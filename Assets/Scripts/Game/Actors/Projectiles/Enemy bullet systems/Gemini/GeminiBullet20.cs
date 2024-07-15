using System.Collections;

public class GeminiBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 1.5f, 1f);
    }
}