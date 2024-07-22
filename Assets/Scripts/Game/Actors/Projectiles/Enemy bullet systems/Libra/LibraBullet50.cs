using System.Collections;

public class LibraBullet50 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
        yield return this.LerpSpeed(0f, 3f, 1f);
    }
}