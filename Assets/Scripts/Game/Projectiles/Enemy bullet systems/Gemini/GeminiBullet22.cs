using System.Collections;

public class GeminiBullet22 : EnemyBullet
{
    protected override float MaxLifetime => 9f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 1.5f, 0.5f);
    }
}