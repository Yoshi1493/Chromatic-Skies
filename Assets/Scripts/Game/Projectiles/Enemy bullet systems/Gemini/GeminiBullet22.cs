using System.Collections;

public class GeminiBullet22 : EnemyBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 2f, 1f);
    }
}