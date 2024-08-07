using System.Collections;

public class LibraBullet65 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 0f, 1f);
    }
}