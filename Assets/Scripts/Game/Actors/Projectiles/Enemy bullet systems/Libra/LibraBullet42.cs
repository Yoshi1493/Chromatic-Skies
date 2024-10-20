using System.Collections;

public class LibraBullet42 : EnemyBullet
{
    protected override float MaxLifetime => 20f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0.5f, 0f, 1f);
        yield return this.LerpSpeed(0f, 1.5f, 1f);
    }
}