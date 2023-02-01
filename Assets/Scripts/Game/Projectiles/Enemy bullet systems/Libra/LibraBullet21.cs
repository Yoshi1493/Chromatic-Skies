using System.Collections;

public class LibraBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 3.5f, 1f);
    }
}