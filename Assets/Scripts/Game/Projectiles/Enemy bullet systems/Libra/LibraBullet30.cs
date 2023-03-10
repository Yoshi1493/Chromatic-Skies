using System.Collections;

public class LibraBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 2f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 9f, 0.6f);
    }
}