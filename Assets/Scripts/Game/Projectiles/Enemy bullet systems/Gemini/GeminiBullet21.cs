using System.Collections;

public class GeminiBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 9f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 3f, 1f);
    }
}