using System.Collections;

public class GeminiBullet23 : EnemyBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 3f, 1f);
    }
}