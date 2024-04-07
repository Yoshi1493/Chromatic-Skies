using System.Collections;

public class LibraBullet31 : EnemyBullet
{
    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(9f, 3f, 0.6f);
    }
}