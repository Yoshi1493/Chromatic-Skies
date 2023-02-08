using System.Collections;

public class LibraBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2.5f, 1f);
    }
}