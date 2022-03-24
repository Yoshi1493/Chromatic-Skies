using System.Collections;

public class LibraBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 2f, 1f);
    }
}