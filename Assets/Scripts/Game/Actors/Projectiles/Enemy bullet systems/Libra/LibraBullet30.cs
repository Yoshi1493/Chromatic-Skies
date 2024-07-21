using System.Collections;

public class LibraBullet30 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 1f);
        yield return this.LerpSpeed(0f, 2f, 1f);
    }
}